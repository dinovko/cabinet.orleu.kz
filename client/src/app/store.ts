// src/app/store.ts
import { configureStore } from "@reduxjs/toolkit";
import profileReducer from "@/features/profile/profileSlice";
import coursesReducer from "@/features/courses/courseSlice";
import groupsReducer from "@/features/groups/groupsSlice";
// import authReducer from '@/features/auth/authSlice';

export const store = configureStore({
  reducer: {
    profile: profileReducer,
    courses: coursesReducer,
    groups: groupsReducer,
    // auth: authReducer,
  },
});

export type RootState = ReturnType<typeof store.getState>;
export type AppDispatch = typeof store.dispatch;
