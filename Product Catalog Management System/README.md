
A full-stack Product Catalog Management System built with ASP.NET Core Web API and Angular. This system allows user to manage 
product inventory through a web interface with features for adding, editing, 
deleting, and searching products, along with hierarchical category management.

By default, the API will be available at:

- `http://localhost:5256`
- `https://localhost:7038`

Swagger UI is available at:

- `http://localhost:5256/swagger`
- `https://localhost:7038/swagger`

### 3. Test Endpoints

You can use Swagger UI or tools like Postman to test the following endpoints:

- `GET /api/products`
- `GET /api/products/{id}`
- `POST /api/products`
- `PUT /api/products/{id}`
- `DELETE /api/products/{id}`
- `GET /api/categories`
- `GET /api/categories/tree`
- `POST /api/categories`

---

## Frontend (Angular SPA)

### 1. Install Dependencies

Navigate to the Angular project directory (e.g., `src/product-catalog-spa`) and run:


The app will be available at:

- `http://localhost:4200`

### 3. API Integration

Ensure the backend is running and CORS is enabled for `http://localhost:4200` in your API (`Program.cs`):


---

## Notes

- The backend uses in-memory repositories; data will reset on restart.
- The Angular app expects the API to be available at `/api/products` and `/api/categories` on `localhost:5256` or `localhost:7038`.
- For production, configure persistent storage and update CORS settings as needed.

---

## Troubleshooting

- **CORS errors:** Ensure CORS is enabled in the backend.
- **Port conflicts:** Change ports in `launchSettings.json` or Angular's `ng serve --port`.
- **API not reachable:** Confirm both backend and frontend are running and using matching ports.

---

## License

MIT
