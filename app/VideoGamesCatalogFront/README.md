# Video Games Catalog

A simple Angular application for managing a video games catalog with CRUD operations.

## Prerequisites

- Node.js 20+
- Angular CLI 17+
- API server running at `https://localhost:7059`

## Installation

```bash
npm install
```

## Development

Start the development server:

```bash
npm start
```

Navigate to `http://localhost:4200`. The application will automatically reload on source file changes.

## Build

```bash
npm run build
```

Build artifacts are stored in `dist/`.

## Testing

Run the unit test suite in headless Chrome without watch mode:

```bash
npm test -- --no-watch --browsers=ChromeHeadless
```

## Regenerate API Client

To regenerate the TypeScript API client from Swagger:

Ignore certificate:

```bash
$env:NODE_TLS_REJECT_UNAUTHORIZED="0"
```

```bash
npm run generate-api
```

**Note:** The API server must be running for this command to work.

Generated files are placed in `src/app/core/api/generated/`. Treat this folder as read-only.
