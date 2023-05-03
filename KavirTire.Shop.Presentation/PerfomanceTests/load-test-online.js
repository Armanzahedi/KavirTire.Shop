import http from "k6/http";
import { check, sleep } from "k6";
import { Rate } from "k6/metrics";
import { htmlReport } from "./reporter.js";

export const options = {
  vus: 5,
  // duration: '10s',
  iterations: 5,
  thresholds: {
    checks: ["rate==1"],
    http_req_duration: ["p(99)<1500"],
  },
};

export const errorRate = new Rate("errors");

export default function () {
  const landingUrl = "https://club.kavirtire.ir/fa-IR/";
  // const loginUrl =
  //   "https://club.kavirtire.ir/fa-IR/SignIn?returnUrl=%2Ffa-IR%2F";
  // const saleInfoUrl = "https://club.kavirtire.ir/fa-IR/SaleInformationContent/";
  // const shoppingUrl = "https://club.kavirtire.ir/shopping";
  // const saveBasketUrl = "https://club.kavirtire.ir/Shop/SaveBasket";
  // const purchaseSummaryUrl = "https://club.kavirtire.ir/PurchaseSummary";

  var res = http.get(landingUrl);
  check(res, { "success landing": (r) => r.status === 200 }) ||
    errorRate.add(1);

  // var res = http.get(loginUrl);
  // check(res, { "success login land": (r) => r.status === 200 }) ||
  //   errorRate.add(1);
  // res = res.submitForm({
  //   formSelector: "form",
  //   fields: { Username: "0493239596", Password: "Aa123456" },
  // });
  // check(res, { "submit login success": (r) => r.status === 200 }) ||
  //   errorRate.add(1);

  // var res = http.get(saleInfoUrl);
  // check(res, { "success saleInfo land": (r) => r.status === 200 }) ||
  //   errorRate.add(1);

  // var res = http.get(shoppingUrl);
  // check(res, { "success shopping land": (r) => r.status === 200 }) ||
  //   errorRate.add(1);

  // const baskets = [
  //   {
  //     ProductId: "bc534a5f-dfb2-e911-9961-005056a2e467",
  //     Price: "5472792",
  //     Quantity: "1",
  //     UnitId: "47a118f7-9337-4ad5-8e6d-938e50ee750f",
  //     TirePostCost: "1000",
  //   },
  // ];

  // const payload = JSON.stringify({ baskets: baskets });
  // const params = {
  //   headers: {
  //     "Content-Type": "application/json",
  //   },
  // };

  // var res = http.post(saveBasketUrl, payload, params);
  // var obj = res.json();

  // check(obj, { "Added to quote successfully": (r) => r.ResultFlag === true }) ||
  //   errorRate.add(1);

  // if (obj.ResultFlag === true) {
  //   var quoteVal = res.cookies["quote-id"]["quote-id"];
  //   var res = http.get(purchaseSummaryUrl, {
  //     cookies: { "quote-id": quoteVal },
  //   });
  //   check(res, { "success purchaseSummary land": (r) => r.status === 200 }) ||
  //     errorRate.add(1);

  //   res = res.submitForm({
  //     formSelector: "form",
  //     fields: { BankAccountId: "2faf97ed-b6b2-ed11-99ef-005056a213f5" },
  //   });
  //   console.log(res.url);
  //   check(res, { "confirmation recieved": (r) => r.url.toLowerCase().includes("paymentconfirmationresult")}) ||    console.log(res.url);
  // }

  sleep(1);
}
export function handleSummary(data) {
  return {
    "summary.html": htmlReport(data),
  };
}
