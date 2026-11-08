# Request Portal (Vite + React)

Employees raise requests; HR approves or rejects them.

## Run

```bash
npm install
npm run dev
```

Open http://localhost:5173, register as **Employee** or **HR**, and log in with the same name + email.

## Notes

- `src/services/*Service.js` use a **localStorage mock** so the app works without a backend.
  To connect a real API, set `VITE_API_URL` (see `.env.example`) and swap the function bodies to use `src/services/api.js`.
- Shared layouts/components:
  - `AuthLayout` + `AuthForm` → Register & Login
  - `DashboardLayout` + `StatsGrid` → Employee & HR dashboards
  - `RequestTable` → Employee "All Requests" & HR "Pending Requests"
  - `RequestDetailView` (+ `RequestActions` for HR) → Employee request details & HR approve page
