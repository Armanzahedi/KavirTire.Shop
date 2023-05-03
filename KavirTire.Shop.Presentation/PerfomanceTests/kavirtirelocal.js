// Creator: WebInspector 537.36

import { sleep, group } from 'k6'
import http from 'k6/http'

export const options = {}

export default function main() {
  let response

  group('page_1 - https://localhost:5000/', function () {
    response = http.get('https://localhost:5000/', {
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
    response = http.get('https://localhost:5000/css/glyphicons-font-awesome-migrate.min.css')
    response = http.get('https://localhost:5000/css/default.bundle.css')
    response = http.get('https://localhost:5000/css/account.css')
    response = http.get('https://localhost:5000/css/fa-custom.css')
    response = http.get('https://localhost:5000/css/custom.css')
    response = http.get('https://localhost:5000/toastr/toastr.min.css')
    response = http.get('https://localhost:5000/images/kavirlogo.png')
    response = http.get('https://localhost:5000/images/users.png')
    response = http.get('https://localhost:5000/js/default.preform.bundle.js')
    response = http.get('https://localhost:5000/webfile/Tornado-1.png', {
      headers: {
        accept: 'image/webp,image/apng,image/svg+xml,image/*,*/*;q=0.8',
        'accept-encoding': 'gzip, deflate, br',
        'accept-language': 'en-US,en;q=0.9,fa;q=0.8',
        cookie:
          'Dynamics365PortalAnalytics=PPaG6EpGJTozpwd1dD4jk_C5S3mPwyNBpfoFOkP1NLia6siCcxeSUUqqMYzOkiFw4A8GECxqHwSU8RnQIPWfAw7SEq_QgkEhjUIb2EbuCVgKU8-Vi0ET09SXx1Ll20L87X4mfwzUyuk1M_9Zmv2eEw2; tof-approved=true',
        referer: 'https://localhost:5000/',
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
    response = http.get('https://localhost:5000/webfile/Tondar%20(R15)-1.png', {
      headers: {
        accept: 'image/webp,image/apng,image/svg+xml,image/*,*/*;q=0.8',
        'accept-encoding': 'gzip, deflate, br',
        'accept-language': 'en-US,en;q=0.9,fa;q=0.8',
        cookie:
          'Dynamics365PortalAnalytics=PPaG6EpGJTozpwd1dD4jk_C5S3mPwyNBpfoFOkP1NLia6siCcxeSUUqqMYzOkiFw4A8GECxqHwSU8RnQIPWfAw7SEq_QgkEhjUIb2EbuCVgKU8-Vi0ET09SXx1Ll20L87X4mfwzUyuk1M_9Zmv2eEw2; tof-approved=true',
        referer: 'https://localhost:5000/',
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
    response = http.get('https://localhost:5000/images/Kavir/lg_enamad_test2.png', {
      headers: {
        accept: 'image/webp,image/apng,image/svg+xml,image/*,*/*;q=0.8',
        'accept-encoding': 'gzip, deflate, br',
        'accept-language': 'en-US,en;q=0.9,fa;q=0.8',
        cookie:
          'Dynamics365PortalAnalytics=PPaG6EpGJTozpwd1dD4jk_C5S3mPwyNBpfoFOkP1NLia6siCcxeSUUqqMYzOkiFw4A8GECxqHwSU8RnQIPWfAw7SEq_QgkEhjUIb2EbuCVgKU8-Vi0ET09SXx1Ll20L87X4mfwzUyuk1M_9Zmv2eEw2; tof-approved=true',
        referer: 'https://localhost:5000/',
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
    response = http.get('https://localhost:5000/images/Kavir/lg_enamad_test.png', {
      headers: {
        accept: 'image/webp,image/apng,image/svg+xml,image/*,*/*;q=0.8',
        'accept-encoding': 'gzip, deflate, br',
        'accept-language': 'en-US,en;q=0.9,fa;q=0.8',
        cookie:
          'Dynamics365PortalAnalytics=PPaG6EpGJTozpwd1dD4jk_C5S3mPwyNBpfoFOkP1NLia6siCcxeSUUqqMYzOkiFw4A8GECxqHwSU8RnQIPWfAw7SEq_QgkEhjUIb2EbuCVgKU8-Vi0ET09SXx1Ll20L87X4mfwzUyuk1M_9Zmv2eEw2; tof-approved=true',
        referer: 'https://localhost:5000/',
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
    response = http.get('https://localhost:5000/assets/bmsd.jpg')
    response = http.get('https://localhost:5000/toastr/toastr.min.js')
    response = http.get('https://localhost:5000/toastr/toastr.custom.js')
    response = http.get('https://localhost:5000/js/general.js')
    response = http.get('https://localhost:5000/js/site.js')
    response = http.get('https://localhost:5000/js/cart.js')
    response = http.get('https://localhost:5000/_vs/browserLink', {
      headers: {
        accept: '*/*',
        'accept-encoding': 'gzip, deflate, br',
        'accept-language': 'en-US,en;q=0.9,fa;q=0.8',
        cookie:
          'Dynamics365PortalAnalytics=PPaG6EpGJTozpwd1dD4jk_C5S3mPwyNBpfoFOkP1NLia6siCcxeSUUqqMYzOkiFw4A8GECxqHwSU8RnQIPWfAw7SEq_QgkEhjUIb2EbuCVgKU8-Vi0ET09SXx1Ll20L87X4mfwzUyuk1M_9Zmv2eEw2; tof-approved=true',
        'if-modified-since': 'Tue, 02 May 2023 07:21:48 GMT',
        referer: 'https://localhost:5000/',
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
    response = http.get('https://localhost:5000/_framework/aspnetcore-browser-refresh.js', {
      headers: {
        accept: '*/*',
        'accept-encoding': 'gzip, deflate, br',
        'accept-language': 'en-US,en;q=0.9,fa;q=0.8',
        cookie:
          'Dynamics365PortalAnalytics=PPaG6EpGJTozpwd1dD4jk_C5S3mPwyNBpfoFOkP1NLia6siCcxeSUUqqMYzOkiFw4A8GECxqHwSU8RnQIPWfAw7SEq_QgkEhjUIb2EbuCVgKU8-Vi0ET09SXx1Ll20L87X4mfwzUyuk1M_9Zmv2eEw2; tof-approved=true',
        referer: 'https://localhost:5000/',
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
    response = http.get('https://localhost:5000/css/~/images/Kavir/loader.gif', {
      headers: {
        accept: 'image/webp,image/apng,image/svg+xml,image/*,*/*;q=0.8',
        'accept-encoding': 'gzip, deflate, br',
        'accept-language': 'en-US,en;q=0.9,fa;q=0.8',
        cookie:
          'Dynamics365PortalAnalytics=PPaG6EpGJTozpwd1dD4jk_C5S3mPwyNBpfoFOkP1NLia6siCcxeSUUqqMYzOkiFw4A8GECxqHwSU8RnQIPWfAw7SEq_QgkEhjUIb2EbuCVgKU8-Vi0ET09SXx1Ll20L87X4mfwzUyuk1M_9Zmv2eEw2; tof-approved=true',
        referer: 'https://localhost:5000/css/default.bundle.css',
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
      'https://localhost:5000/fonts/Kavir/fontawesome/fontawesome-webfont.woff2?v=4.7.0'
    )
    response = http.get(
      'https://localhost:5000/fonts/iransans//fonts/woff2/IRANSansWeb(FaNum).woff2'
    )
    response = http.get('https://localhost:5000/assets/empty-basket.png')
    response = http.get('https://localhost:5000/assets/icon_basket.png')
    response = http.get('https://localhost:5000/assets/close.png')
    response = http.get(
      'https://app.raychat.io/scripts/js/2303eb12-3f26-4c78-ad5d-fb143e62bf5f?rid=64314b85bf8a3e0e380bc1e3&href=https://localhost:5000/',
      {
        headers: {
          accept: '*/*',
          'accept-encoding': 'gzip, deflate, br',
          'accept-language': 'en-US,en;q=0.9,fa;q=0.8',
          referer: 'https://localhost:5000/',
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
    response = http.get(
      'http://localhost:62729/b8d763ea1a7a498888267d553c5ede98/browserLinkSignalR/negotiate?requestUrl=https%3A%2F%2Flocalhost%3A5000%2F&browserName=&userAgent=Mozilla%2F5.0+%28Windows+NT+10.0%3B+Win64%3B+x64%29+AppleWebKit%2F537.36+%28KHTML%2C+like+Gecko%29+Chrome%2F112.0.0.0+Safari%2F537.36+Edg%2F112.0.1722.64&browserIdKey=window.browserLink.initializationData.browserId&browserId=39aa-f82f&clientProtocol=1.3&_=1683012140909&userAgent=Mozilla%252F5.0%2B%28Windows%2BNT%2B10.0%253B%2BWin64%253B%2Bx64%29%2BAppleWebKit%252F537.36%2B%28KHTML%252C%2Blike%2BGecko%29%2BChrome%252F112.0.0.0%2BSafari%252F537.36%2BEdg%252F112.0.1722.64',
      {
        headers: {
          Accept: 'text/plain, */*; q=0.01',
          'Accept-Encoding': 'gzip, deflate, br',
          'Accept-Language': 'en-US,en;q=0.9,fa;q=0.8',
          Connection: 'keep-alive',
          'Content-Type': 'application/x-www-form-urlencoded; charset=UTF-8',
          Host: 'localhost:62729',
          Origin: 'https://localhost:5000',
          'Sec-Fetch-Dest': 'empty',
          'Sec-Fetch-Mode': 'cors',
          'Sec-Fetch-Site': 'cross-site',
          'User-Agent':
            'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/112.0.0.0 Safari/537.36 Edg/112.0.1722.64',
          'sec-ch-ua': '"Chromium";v="112", "Microsoft Edge";v="112", "Not:A-Brand";v="99"',
          'sec-ch-ua-mobile': '?0',
          'sec-ch-ua-platform': '"Windows"',
        },
      }
    )
    response = http.get('https://localhost:5000/favicon.ico')
    response = http.get(
      'ws://localhost:62729/b8d763ea1a7a498888267d553c5ede98/browserLinkSignalR/connect?transport=webSockets&connectionToken=AQAAANCMnd8BFdERjHoAwE%2FCl%2BsBAAAA7iJLWXhmeUuYQXO1nn541AAAAAACAAAAAAADZgAAwAAAABAAAADIX9axlFuRHn5Mv357VKpWAAAAAASAAACgAAAAEAAAAF1XgGeW5p6q4DbIQ269RkUoAAAAkcuYdwwSunbcZd3OLOKhT2MJDmVuVf%2BbcJhENtym%2F06lxl10hCUp5hQAAAA9XgFPRk3F75GWPxWyOgsHzCBocg%3D%3D&requestUrl=https%3A%2F%2Flocalhost%3A5000%2F&browserName=&userAgent=Mozilla%2F5.0+%28Windows+NT+10.0%3B+Win64%3B+x64%29+AppleWebKit%2F537.36+%28KHTML%2C+like+Gecko%29+Chrome%2F112.0.0.0+Safari%2F537.36+Edg%2F112.0.1722.64&browserIdKey=window.browserLink.initializationData.browserId&browserId=39aa-f82f&tid=6&userAgent=Mozilla%252F5.0%2B%28Windows%2BNT%2B10.0%253B%2BWin64%253B%2Bx64%29%2BAppleWebKit%252F537.36%2B%28KHTML%252C%2Blike%2BGecko%29%2BChrome%252F112.0.0.0%2BSafari%252F537.36%2BEdg%252F112.0.1722.64',
      {
        headers: {
          Pragma: 'no-cache',
          Origin: 'https://localhost:5000',
          'Accept-Encoding': 'gzip, deflate, br',
          Host: 'localhost:62729',
          'Accept-Language': 'en-US,en;q=0.9,fa;q=0.8',
          'Sec-WebSocket-Key': 'lIwjthQpmnGQ5UXV9vRUaA==',
          'User-Agent':
            'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/112.0.0.0 Safari/537.36 Edg/112.0.1722.64',
          Upgrade: 'websocket',
          'Cache-Control': 'no-cache',
          Connection: 'Upgrade',
          'Sec-WebSocket-Version': '13',
          'Sec-WebSocket-Extensions': 'permessage-deflate; client_max_window_bits',
        },
      }
    )
    response = http.get('wss://localhost:44368/KavirTire.Shop.Presentation/', {
      headers: {
        Pragma: 'no-cache',
        Origin: 'https://localhost:5000',
        'Accept-Encoding': 'gzip, deflate, br',
        Host: 'localhost:44368',
        'Accept-Language': 'en-US,en;q=0.9,fa;q=0.8',
        'Sec-WebSocket-Key': 'kBILihj29hxyUzItnFgSqQ==',
        'User-Agent':
          'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/112.0.0.0 Safari/537.36 Edg/112.0.1722.64',
        Upgrade: 'websocket',
        'Cache-Control': 'no-cache',
        'Sec-WebSocket-Protocol':
          'hJj%2FHlpGzoUU08C90edbCZj9RM7hXg2k3GmF4%2FM77egfSmN9oYm59v0uSMjYDzIggRIPEaGnxDeKxAzyL4IKqKVVL9oz%2BhbaFRDGCtIkDRbYPOMhsAlaZ%2FLw08yfdbQNKGullu5YlW4wH%2BdeDSgMy2fc%2FbYTNXIyqZuJiGj%2FvYBvDzcxsWScT9Q5bxoEeOb0zTVaH1EC7vpKROnvpy%2F6rIwdp4vYKH94ud8uzPn1%2FFSs%2FaCPSHfwaprbFYL3J0kepVvTB3RtqJXGUvPADtSCX%2BzBWDEsU0qTtrDy6vHspHJuqT1dTsDvq%2FBMf9bIWgPHWFg%2Fxm3AU4CVBY23N0oY3A%3D%3D',
        Connection: 'Upgrade',
        'Sec-WebSocket-Version': '13',
        'Sec-WebSocket-Extensions': 'permessage-deflate; client_max_window_bits',
      },
    })
  })

  // Automatically added sleep
  sleep(1)
}
