import http from "k6/http";
import { check, sleep } from "k6";
import { Rate } from "k6/metrics";


export const options = {
    vus: 2,
    // duration: '10s',
    iterations:2,
    thresholds: {
      checks: ['rate==1'],
    },
  };

export const errorRate = new Rate("errors");

export default function () {
  const landingUrl = "http://localhost:53000/fa-IR/";
  const loginUrl = "http://localhost:53000/fa-IR/SignIn?returnUrl=%2Ffa-IR%2F";
  const saleInfoUrl = "http://localhost:53000/fa-IR/SaleInformationContent/";
  const shoppingUrl = "http://localhost:53000/shopping";
  const saveBasketUrl = "http://localhost:53000/Shop/SaveBasket";
  const purchaseSummaryUrl = "http://localhost:53000/PurchaseSummary";

  var res = http.get(landingUrl);
  check(res, { "success landing": (r) => r.status === 200 }) ||
    errorRate.add(1);

  var res = http.get(loginUrl);
  check(res, { "success login land": (r) => r.status === 200 }) ||
    errorRate.add(1);

  res = res.submitForm({
    formSelector: "form",
    // fields: { Username: "sohrabi", Password: "Xx*123456" },
    fields: { Username: "3240384205", Password: "Aa123456" },
  });
  check(res, { "submit login success": (r) => r.status === 200 }) ||
    errorRate.add(1);

  var res = http.get(saleInfoUrl);
  check(res, { "success saleInfo land": (r) => r.status === 200 }) ||
    errorRate.add(1);

  var res = http.get(shoppingUrl);
  check(res, { "success shopping land": (r) => r.status === 200 }) ||
    errorRate.add(1);

  const baskets = [
    {
      ProductId: "0443954e-722f-e911-8126-0050569abf13",
      Price: "5472792",
      Quantity: "1",
      UnitId: "bef8f751-a37c-47ca-a1cd-57d0024d2a4a",
      TirePostCost: "332307",
    },
  ];

  const payload = JSON.stringify({ baskets: baskets });
  const params = {
    headers: {
      "Content-Type": "application/json",
    },
  };

  var res = http.post(saveBasketUrl, payload, params);
  var obj = res.json();

  check(obj, { "Added to quote successfully": (r) => r.ResultFlag === true }) ||
    errorRate.add(1);

  if (obj.ResultFlag === true) {
    var quoteVal = res.cookies["quote-id"][0]["value"];
    console.log(quoteVal)
    var res = http.get(purchaseSummaryUrl, {
      cookies: { "quote-id": quoteVal },
    });
    check(res, { "success purchaseSummary land": (r) => r.status === 200 }) ||
      errorRate.add(1);

    res = res.submitForm({
      formSelector: "form",
      fields: { BankAccountId: "b8b122c3-c7b1-ed11-81e7-0050569abf13" },
    });
    console.log(res.url);
    check(res, { "confirmation recieved": (r) => r.url.toLowerCase().includes("paymentconfirmationresult")}) || errorRate.add(1);
  }

  sleep(1);
}
