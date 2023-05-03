import http from "k6/http";
import { check, sleep } from "k6";
import { Rate } from "k6/metrics";

export const options = {
  vus: 1000,
  // duration: '10s',
  iterations: 1000,
  thresholds: {
    checks: ["rate==1"],
    http_req_duration: ['p(99)<15000'],
  },
};

export const errorRate = new Rate("errors");

export default function () {
  const landingUrl = "https://localhost:44301/";

  var res = http.get(landingUrl);
  check(res, { "success landing": (r) => r.status === 200 }) ||
    errorRate.add(1);
  sleep(1);
}
