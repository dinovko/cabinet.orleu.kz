import { API_URL } from "@/shared/constants";
import type { ICourses } from "./types";

export const courseAPI = {
  async getCurrentUserCourses(): Promise<ICourses[]> {
    // Пример запроса — замени на реальный API
    const response = await fetch(`${API_URL}/api/Account/courses`, {
      credentials: 'include',
    });
    if (!response.ok) throw new Error('Failed to load profile');
    return await response.json();
  },
};
