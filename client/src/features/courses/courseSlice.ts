// src/store/profileSlice.ts
import { createSlice, createAsyncThunk } from "@reduxjs/toolkit";
import type { ICourses } from "./types";
import { courseAPI } from "./courseAPI";

interface CoursesState {
  data: ICourses[] | null;
  status: "idle" | "loading" | "succeeded" | "failed";
  loading: boolean;
  error: string | null;
  lastFetched: number | null;
}

const initialState: CoursesState = {
  data: [],
  status: "idle",
  loading: false,
  error: null,
  lastFetched: null,
};

const CACHE_DURATION = 5 * 60 * 1000;

export const fetchCurrentUserCourses = createAsyncThunk<
  ICourses[],
  void,
  { state: { courses: CoursesState } }
>(
  "profile/fetchCurrentUserCourses",
  async (_, thunkAPI) => {
    try {
      return await courseAPI.getCurrentUserCourses();
    } catch (err: any) {
      return thunkAPI.rejectWithValue(err.message);
    }
  },
  {
    condition: (_, { getState }) => {
      const { lastFetched, status } = getState().courses;
      const now = Date.now();

      if (status === "loading") return false;

      if (lastFetched && now - lastFetched < CACHE_DURATION) {
        // пока время кэша норм
        return false;
      }

      return true;
    },
  }
);

const courseSlice = createSlice({
  name: "course",
  initialState,
  reducers: {},
  extraReducers: (builder) => {
    builder
      .addCase(fetchCurrentUserCourses.pending, (state) => {
        state.loading = true;
        state.error = null;
      })
      .addCase(fetchCurrentUserCourses.fulfilled, (state, action) => {
        state.data = action.payload;
        state.loading = false;
        state.lastFetched = Date.now();
      })
      .addCase(fetchCurrentUserCourses.rejected, (state, action) => {
        state.loading = false;
        state.error = action.payload as string;
      });
  },
});

export default courseSlice.reducer;
