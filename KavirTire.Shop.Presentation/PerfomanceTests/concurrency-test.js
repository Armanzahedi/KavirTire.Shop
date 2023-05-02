import http from "k6/http";
import { sleep } from "k6";


export const options = {
    scenarios: {
        stress: {
            executor: "ramping-arrival-rate",
            preAllocatedVUs: 4000,
            timeUnit: "1s",
            stages: [
                { duration: "10s", target: 1 }, // below normal load
                //{ duration: "10s", target: 100 },
                //{ duration: "30s", target: 100 }, // normal load
                //{ duration: "10s", target: 500 },
                //{ duration: "30s", target: 500 }, // around the breaking point
                //// { duration: "10s", target: 3000 },
                //// { duration: "30s", target: 3000 }, // beyond the breaking point
                //{ duration: "10s", target: 40 },
                //{ duration: "10s", target: 0 }, // scale down. Recovery stage.
            ],
        },
    },
    thresholds: {
        checks: ["rate==1"],
        http_req_duration: ['p(99)<15000'],
    },
};

export default function () {
    let data = { 
        BankAccountId: 'f429fb15-90e4-eb11-8199-0050569abf13',
        InvoiceId:'EDD71DA5-AFDA-4223-5EF6-08DB497A7745'
    };
    var res = http.post("https://localhost:7219/Payment", JSON.stringify(data), {
        headers: { 'Content-Type': 'application/json' },
    });
    console.log(res.json().paymentId);
  sleep(1); 
}
