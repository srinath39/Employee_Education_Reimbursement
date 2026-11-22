import { ROLES } from "../utils/constants";

export async function register({ name, email, role }) {
  if (name === "srinath") {
    return { userId: "f3c7a1d2-4b5e-4c8a-9d2f-1a2b3c4d5e01", role: ROLES.EMPLOYEE }
  } else {
    return { userId: "f3c7a1d2-4b5e-4c8a-9d2f-1a2b3c4d5e04", role: ROLES.HR }
  }
}

export async function login({ name, email }) {
  if (name === "srinath") {
    return { userId: "f3c7a1d2-4b5e-4c8a-9d2f-1a2b3c4d5e01", role: ROLES.EMPLOYEE }
  } else {
    return { userId: "f3c7a1d2-4b5e-4c8a-9d2f-1a2b3c4d5e04", role: ROLES.HR }
  }
}
