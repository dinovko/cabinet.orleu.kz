import { API_URL } from "@/shared/constants";
import type { IUserProfile } from "./types";

export const profileAPI = {
  async getCurrentUser(): Promise<IUserProfile|any> {
    try {
      const response = await fetch(`${API_URL}/api/ebds/userprofile`, {
        credentials: 'include',
        redirect: 'manual',
      });
      if(response.status === 401) {
        window.location.href = `${API_URL}`;
      }
      if (!response.ok) throw new Error('Failed to load profile');
      return await response.json();
    } catch (error:any) {
      console.info(JSON.stringify(error));      
    }
    // Пример запроса — замени на реальный API
  },
};
