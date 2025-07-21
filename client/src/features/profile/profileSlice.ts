// src/store/profileSlice.ts
import { createSlice, createAsyncThunk } from '@reduxjs/toolkit';
import type { IUserProfile } from './types';
import { profileAPI } from './profileAPI';

interface ProfileState {
  user: IUserProfile | null;
  loading: boolean;
  error: string | null;
}

const initialState: ProfileState = {
  user: null,
  loading: false,
  error: null,
};

export const fetchCurrentUser = createAsyncThunk(
  'profile/fetchCurrentUser',
  async (_, thunkAPI) => {
    try {
      return await profileAPI.getCurrentUser();
    } catch (err: any) {
      return thunkAPI.rejectWithValue(err.message);
    }
  }
);

const profileSlice = createSlice({
  name: 'profile',
  initialState,
  reducers: {},
  extraReducers: (builder) => {
    builder
      .addCase(fetchCurrentUser.pending, (state) => {
        state.loading = true;
        state.error = null;
      })
      .addCase(fetchCurrentUser.fulfilled, (state, action) => {
        state.user = action.payload;
        state.loading = false;
      })
      .addCase(fetchCurrentUser.rejected, (state, action) => {
        state.loading = false;
        state.error = action.payload as string;
      });
  },
});

export default profileSlice.reducer;
