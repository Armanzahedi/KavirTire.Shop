import http from 'k6/http';


export const options = {
    scenarios: {
        stress: {
            executor: "ramping-arrival-rate",
            preAllocatedVUs: 4000,
            timeUnit: "1s",
            stages: [
                { duration: "10s", target: 10 }, // below normal load
                { duration: "10s", target: 100 },
                { duration: "30s", target: 100 }, // normal load
                { duration: "10s", target: 1000 },
                { duration: "30s", target: 1000 }, // around the breaking point
                // { duration: "10s", target: 3000 },
                // { duration: "30s", target: 3000 }, // beyond the breaking point
                { duration: "10s", target: 40 },
                { duration: "10s", target: 0 }, // scale down. Recovery stage.
            ],
        },
    },
    thresholds: {
        checks: ["rate==1"],
        http_req_duration: ['p(99)<15000'],
    },
};

export default function () {
    const landingUrl = "https://localhost:7219";
    http.get(landingUrl);
}
