import { API_URL } from "@/shared/constants";
import type { IGroup } from "./types";

export const groupsAPI = {
  async getCurrentUserGroups(): Promise<IGroup> {
    // Пример запроса — замени на реальный API
    const response = await fetch(`${API_URL}/api/qr/groups`, {
      credentials: "include",
    });
    if (!response.ok) throw new Error("Failed to load profile");
    return await response.json();
  },
};
