
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
        { duration: "30s", target: 1000 }, 
        { duration: "1m", target: 1000 }, 
        { duration: "30s", target: 2000 }, 
        { duration: "1m", target: 2000 }, 
        { duration: "30s", target: 1000 }, 
        { duration: "30s", target: 1000 }, 
        { duration: "30s", target: 100 },
        { duration: "30s", target: 100 },
        { duration: "30s", target: 0 },
      ],
};

export const errorRate = new Rate("errors");

export default function () {
let response

  group('page_1 - http://srv-crm-cms:5000/', function () {
    response = http.get('http://srv-crm-cms:5000/', {
      headers: {
        accept:
          'text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.7',
        'accept-encoding': 'gzip, deflate, br',
        'accept-language': 'en-US,en;q=0.9,fa;q=0.8',
        'cache-control': 'max-age=0',
        cookie:
          'Dynamics365PortalAnalytics=PPaG6EpGJTozpwd1dD4jk_C5S3mPwyNBpfoFOkP1NLia6siCcxeSUUqqMYzOkiFw4A8GECxqHwSU8RnQIPWfAw7SEq_QgkEhjUIb2EbuCVgKU8-Vi0ET09SXx1Ll20L87X4mfwzUyuk1M_9Zmv2eEw2; tof-approved=true',
        'sec-ch-ua': '"Chromium";v="112", "Microsoft Edge";v="112", "Not:A-Brand";v="99"',
        'sec-ch-ua-mobile': '?0',
        'sec-ch-ua-platform': '"Windows"',
        'sec-fetch-dest': 'document',
        'sec-fetch-mode': 'navigate',
        'sec-fetch-site': 'none',
        'sec-fetch-user': '?1',
        'upgrade-insecure-requests': '1',
        'user-agent':
          'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/112.0.0.0 Safari/537.36 Edg/112.0.1722.64',
      },
    })
    response = http.get('http://srv-crm-cms:5000/css/glyphicons-font-awesome-migrate.min.css')
    response = http.get('http://srv-crm-cms:5000/css/default.bundle.css')
    response = http.get('http://srv-crm-cms:5000/css/account.css')
    response = http.get('http://srv-crm-cms:5000/css/fa-custom.css')
    response = http.get('http://srv-crm-cms:5000/css/custom.css')
    response = http.get('http://srv-crm-cms:5000/toastr/toastr.min.css')
    response = http.get('http://srv-crm-cms:5000/images/kavirlogo.png')
    response = http.get('http://srv-crm-cms:5000/images/users.png')
    response = http.get('http://srv-crm-cms:5000/js/default.preform.bundle.js')
    response = http.get('http://srv-crm-cms:5000/webfile/Tornado-1.png', {
      headers: {
        accept: 'image/webp,image/apng,image/svg+xml,image/*,*/*;q=0.8',
        'accept-encoding': 'gzip, deflate, br',
        'accept-language': 'en-US,en;q=0.9,fa;q=0.8',
        cookie:
          'Dynamics365PortalAnalytics=PPaG6EpGJTozpwd1dD4jk_C5S3mPwyNBpfoFOkP1NLia6siCcxeSUUqqMYzOkiFw4A8GECxqHwSU8RnQIPWfAw7SEq_QgkEhjUIb2EbuCVgKU8-Vi0ET09SXx1Ll20L87X4mfwzUyuk1M_9Zmv2eEw2; tof-approved=true',
        referer: 'http://srv-crm-cms:5000/',
        'sec-ch-ua': '"Chromium";v="112", "Microsoft Edge";v="112", "Not:A-Brand";v="99"',
        'sec-ch-ua-mobile': '?0',
        'sec-ch-ua-platform': '"Windows"',
        'sec-fetch-dest': 'image',
        'sec-fetch-mode': 'no-cors',
        'sec-fetch-site': 'same-origin',
        'user-agent':
          'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/112.0.0.0 Safari/537.36 Edg/112.0.1722.64',
      },
    })
    response = http.get('http://srv-crm-cms:5000/webfile/Tondar%20(R15)-1.png', {
      headers: {
        accept: 'image/webp,image/apng,image/svg+xml,image/*,*/*;q=0.8',
        'accept-encoding': 'gzip, deflate, br',
        'accept-language': 'en-US,en;q=0.9,fa;q=0.8',
        cookie:
          'Dynamics365PortalAnalytics=PPaG6EpGJTozpwd1dD4jk_C5S3mPwyNBpfoFOkP1NLia6siCcxeSUUqqMYzOkiFw4A8GECxqHwSU8RnQIPWfAw7SEq_QgkEhjUIb2EbuCVgKU8-Vi0ET09SXx1Ll20L87X4mfwzUyuk1M_9Zmv2eEw2; tof-approved=true',
        referer: 'http://srv-crm-cms:5000/',
        'sec-ch-ua': '"Chromium";v="112", "Microsoft Edge";v="112", "Not:A-Brand";v="99"',
        'sec-ch-ua-mobile': '?0',
        'sec-ch-ua-platform': '"Windows"',
        'sec-fetch-dest': 'image',
        'sec-fetch-mode': 'no-cors',
        'sec-fetch-site': 'same-origin',
        'user-agent':
          'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/112.0.0.0 Safari/537.36 Edg/112.0.1722.64',
      },
    })
    response = http.get('http://srv-crm-cms:5000/images/Kavir/lg_enamad_test2.png', {
      headers: {
        accept: 'image/webp,image/apng,image/svg+xml,image/*,*/*;q=0.8',
        'accept-encoding': 'gzip, deflate, br',
        'accept-language': 'en-US,en;q=0.9,fa;q=0.8',
        cookie:
          'Dynamics365PortalAnalytics=PPaG6EpGJTozpwd1dD4jk_C5S3mPwyNBpfoFOkP1NLia6siCcxeSUUqqMYzOkiFw4A8GECxqHwSU8RnQIPWfAw7SEq_QgkEhjUIb2EbuCVgKU8-Vi0ET09SXx1Ll20L87X4mfwzUyuk1M_9Zmv2eEw2; tof-approved=true',
        referer: 'http://srv-crm-cms:5000/',
        'sec-ch-ua': '"Chromium";v="112", "Microsoft Edge";v="112", "Not:A-Brand";v="99"',
        'sec-ch-ua-mobile': '?0',
        'sec-ch-ua-platform': '"Windows"',
        'sec-fetch-dest': 'image',
        'sec-fetch-mode': 'no-cors',
        'sec-fetch-site': 'same-origin',
        'user-agent':
          'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/112.0.0.0 Safari/537.36 Edg/112.0.1722.64',
      },
    })
    response = http.get('http://srv-crm-cms:5000/images/Kavir/lg_enamad_test.png', {
      headers: {
        accept: 'image/webp,image/apng,image/svg+xml,image/*,*/*;q=0.8',
        'accept-encoding': 'gzip, deflate, br',
        'accept-language': 'en-US,en;q=0.9,fa;q=0.8',
        cookie:
          'Dynamics365PortalAnalytics=PPaG6EpGJTozpwd1dD4jk_C5S3mPwyNBpfoFOkP1NLia6siCcxeSUUqqMYzOkiFw4A8GECxqHwSU8RnQIPWfAw7SEq_QgkEhjUIb2EbuCVgKU8-Vi0ET09SXx1Ll20L87X4mfwzUyuk1M_9Zmv2eEw2; tof-approved=true',
        referer: 'http://srv-crm-cms:5000/',
        'sec-ch-ua': '"Chromium";v="112", "Microsoft Edge";v="112", "Not:A-Brand";v="99"',
        'sec-ch-ua-mobile': '?0',
        'sec-ch-ua-platform': '"Windows"',
        'sec-fetch-dest': 'image',
        'sec-fetch-mode': 'no-cors',
        'sec-fetch-site': 'same-origin',
        'user-agent':
          'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/112.0.0.0 Safari/537.36 Edg/112.0.1722.64',
      },
    })
    response = http.get('http://srv-crm-cms:5000/assets/bmsd.jpg')
    response = http.get('http://srv-crm-cms:5000/toastr/toastr.min.js')
    response = http.get('http://srv-crm-cms:5000/toastr/toastr.custom.js')
    response = http.get('http://srv-crm-cms:5000/js/general.js')
    response = http.get('http://srv-crm-cms:5000/js/site.js')
    response = http.get('http://srv-crm-cms:5000/js/cart.js')
    response = http.get('http://srv-crm-cms:5000/_vs/browserLink', {
      headers: {
        accept: '*/*',
        'accept-encoding': 'gzip, deflate, br',
        'accept-language': 'en-US,en;q=0.9,fa;q=0.8',
        cookie:
          'Dynamics365PortalAnalytics=PPaG6EpGJTozpwd1dD4jk_C5S3mPwyNBpfoFOkP1NLia6siCcxeSUUqqMYzOkiFw4A8GECxqHwSU8RnQIPWfAw7SEq_QgkEhjUIb2EbuCVgKU8-Vi0ET09SXx1Ll20L87X4mfwzUyuk1M_9Zmv2eEw2; tof-approved=true',
        'if-modified-since': 'Tue, 02 May 2023 07:21:48 GMT',
        referer: 'http://srv-crm-cms:5000/',
        'sec-ch-ua': '"Chromium";v="112", "Microsoft Edge";v="112", "Not:A-Brand";v="99"',
        'sec-ch-ua-mobile': '?0',
        'sec-ch-ua-platform': '"Windows"',
        'sec-fetch-dest': 'script',
        'sec-fetch-mode': 'no-cors',
        'sec-fetch-site': 'same-origin',
        'user-agent':
          'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/112.0.0.0 Safari/537.36 Edg/112.0.1722.64',
      },
    })
    response = http.get('http://srv-crm-cms:5000/_framework/aspnetcore-browser-refresh.js', {
      headers: {
        accept: '*/*',
        'accept-encoding': 'gzip, deflate, br',
        'accept-language': 'en-US,en;q=0.9,fa;q=0.8',
        cookie:
          'Dynamics365PortalAnalytics=PPaG6EpGJTozpwd1dD4jk_C5S3mPwyNBpfoFOkP1NLia6siCcxeSUUqqMYzOkiFw4A8GECxqHwSU8RnQIPWfAw7SEq_QgkEhjUIb2EbuCVgKU8-Vi0ET09SXx1Ll20L87X4mfwzUyuk1M_9Zmv2eEw2; tof-approved=true',
        referer: 'http://srv-crm-cms:5000/',
        'sec-ch-ua': '"Chromium";v="112", "Microsoft Edge";v="112", "Not:A-Brand";v="99"',
        'sec-ch-ua-mobile': '?0',
        'sec-ch-ua-platform': '"Windows"',
        'sec-fetch-dest': 'script',
        'sec-fetch-mode': 'no-cors',
        'sec-fetch-site': 'same-origin',
        'user-agent':
          'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/112.0.0.0 Safari/537.36 Edg/112.0.1722.64',
      },
    })
    response = http.get('http://srv-crm-cms:5000/css/~/images/Kavir/loader.gif', {
      headers: {
        accept: 'image/webp,image/apng,image/svg+xml,image/*,*/*;q=0.8',
        'accept-encoding': 'gzip, deflate, br',
        'accept-language': 'en-US,en;q=0.9,fa;q=0.8',
        cookie:
          'Dynamics365PortalAnalytics=PPaG6EpGJTozpwd1dD4jk_C5S3mPwyNBpfoFOkP1NLia6siCcxeSUUqqMYzOkiFw4A8GECxqHwSU8RnQIPWfAw7SEq_QgkEhjUIb2EbuCVgKU8-Vi0ET09SXx1Ll20L87X4mfwzUyuk1M_9Zmv2eEw2; tof-approved=true',
        referer: 'http://srv-crm-cms:5000/css/default.bundle.css',
        'sec-ch-ua': '"Chromium";v="112", "Microsoft Edge";v="112", "Not:A-Brand";v="99"',
        'sec-ch-ua-mobile': '?0',
        'sec-ch-ua-platform': '"Windows"',
        'sec-fetch-dest': 'image',
        'sec-fetch-mode': 'no-cors',
        'sec-fetch-site': 'same-origin',
        'user-agent':
          'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/112.0.0.0 Safari/537.36 Edg/112.0.1722.64',
      },
    })
    response = http.get(
      'http://srv-crm-cms:5000/fonts/Kavir/fontawesome/fontawesome-webfont.woff2?v=4.7.0'
    )
    response = http.get(
      'http://srv-crm-cms:5000/fonts/iransans//fonts/woff2/IRANSansWeb(FaNum).woff2'
    )
    response = http.get('http://srv-crm-cms:5000/assets/empty-basket.png')
    response = http.get('http://srv-crm-cms:5000/assets/icon_basket.png')
    response = http.get('http://srv-crm-cms:5000/assets/close.png')
    response = http.get(
      'http://app.raychat.io/scripts/js/2303eb12-3f26-4c78-ad5d-fb143e62bf5f?rid=64314b85bf8a3e0e380bc1e3&href=http://srv-crm-cms:5000/',
      {
        headers: {
          accept: '*/*',
          'accept-encoding': 'gzip, deflate, br',
          'accept-language': 'en-US,en;q=0.9,fa;q=0.8',
          referer: 'http://srv-crm-cms:5000/',
          'sec-ch-ua': '"Chromium";v="112", "Microsoft Edge";v="112", "Not:A-Brand";v="99"',
          'sec-ch-ua-mobile': '?0',
          'sec-ch-ua-platform': '"Windows"',
          'sec-fetch-dest': 'script',
          'sec-fetch-mode': 'no-cors',
          'sec-fetch-site': 'cross-site',
          'user-agent':
            'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/112.0.0.0 Safari/537.36 Edg/112.0.1722.64',
        },
      }
    )
    response = http.get('http://srv-crm-cms:5000/favicon.ico')
  })

  // Automatically added sleep
sleep(1);
}
// export function handleSummary(data) {
//   return {
//     "summary.html": htmlReport(data),
//   };
// }