import { createSlice, createAsyncThunk } from "@reduxjs/toolkit";
import type { IGroup } from "./types";
import { groupsAPI } from "./groupsAPI";

interface GroupsState {
  data: IGroup | null;
  status: "idle" | "loading" | "succeeded" | "failed";
  loading: boolean;
  error: string | null;
  lastFetched: number | null;
}

const initialState: GroupsState = {
  data: null,
  status: "idle",
  loading: false,
  error: null,
  lastFetched: null,
};

const CACHE_DURATION = 5 * 60 * 1000;

export const fetchCurrentUserGroups = createAsyncThunk(
  "profile/fetchCurrentUserGroups",
  async (_, thunkAPI) => {
    try {
      return await groupsAPI.getCurrentUserGroups();
    } catch (err: any) {
      return thunkAPI.rejectWithValue(err.message);
    }
  }
);

const groupsSlice = createSlice({
  name: "groups",
  initialState,
  reducers: {},
  extraReducers: (builder) => {
    builder
      .addCase(fetchCurrentUserGroups.pending, (state) => {
        state.loading = true;
        state.error = null;
      })
      .addCase(fetchCurrentUserGroups.fulfilled, (state, action) => {
        state.data = action.payload;
        state.loading = false;
        state.lastFetched = Date.now();
      })
      .addCase(fetchCurrentUserGroups.rejected, (state, action) => {
        state.loading = false;
        state.error = action.payload as string;
      });
  },
});

export default groupsSlice.reducer;
