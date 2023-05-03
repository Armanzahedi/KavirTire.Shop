import { chromium } from 'k6/experimental/browser';

import http from "k6/http";
import { check, sleep,group } from "k6";
import { Rate } from "k6/metrics";
import { htmlReport } from "./reporter.js";

export const options = {
 thresholds: {
			http_req_duration: ['p(95)<5000'], // 95% of requests must complete within 5 seconds
		  },
      stages: [
        { duration: "30s", target: 100 },
        { duration: "30s", target: 100 },
        { duration: "30s", target: 500 },
        { duration: "30s", target: 500 },
        // { duration: "30s", target: 2000 },
        // { duration: "1m", target: 2000 },
        // { duration: "30s", target: 1000 },
        // { duration: "30s", target: 1000 },
        // { duration: "30s", target: 100 },
        { duration: "30s", target: 100 },
        { duration: "30s", target: 0 },
      ],
};

export const errorRate = new Rate("errors");

export default async function () {
    const browser = chromium.launch({ headless: true });
    const page = browser.newPage();

  try {
    await page.goto('http://srv-crm-cms:5000', { waitUntil: 'networkidle' });

    check(page, {
      'header': page.locator('h6').textContent() == 'TORNADO 195/65 R15',
    });
  } finally {
    page.close();
    browser.close();
  }
sleep(1);
}
// export function handleSummary(data) {
//   return {
//     "summary.html": htmlReport(data),
//   };
// }