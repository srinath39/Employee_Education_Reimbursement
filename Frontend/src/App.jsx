import AppRoutes from "./routes/AppRoutes";

export default function App() {
  return <AppRoutes />;

  //component -based architecture/ seperation of concern / Layered architecture 

  // pages /       → screens(one per route)
  // components /  → reusable UI pieces
  // layouts /     → page shells
  // hooks /       → reusable logic(useAuth, useRequests)
  // context /     → global state(who is logged in)
  // services /    → API calls(the data layer)
  // utils /       → helpers and constants
}
