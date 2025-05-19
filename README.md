# qurlify-api

qurlify-api is a modern URL shortener service built with C#. It not only shortens long URLs, but also generates a unique QR code for each link and allows you to display a custom message to anyone who receives or opens your shortened URL.

## Features

- **URL Shortening**: Convert long and unwieldy URLs into compact, shareable links.
- **QR Code Generation**: Every shortened URL comes with an automatically generated QR code for easy sharing and scanning.
- **Custom Messages**: Attach a personalized message to your link, which will be shown to users who open or scan the QR code.
- **RESTful API**: Built with modern C# practices for easy integration with other applications or frontends.

## How It Works

1. **Shorten a URL**: Submit a long URL (with an optional custom message) to the API.
2. **Receive your short link & QR**: The API returns a shortened URL and a QR code image.
3. **Share**: Distribute your short link or QR code.
4. **User experience**: When someone visits or scans your short link, they see your custom message before being redirected to the destination URL.

## Example Usage

```http
POST /api/shorten
Content-Type: application/json

{
  "url": "https://verylongurl.com/page?with=query",
  "message": "Hi there! Thanks for using qurlify."
}
