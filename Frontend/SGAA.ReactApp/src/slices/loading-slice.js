import { createSlice } from "@reduxjs/toolkit";

const initialState = {
  loadingCount: 0
};

export const loadingSlice = createSlice({
  name: "loading",
  initialState,
  reducers: {
    startLoading: (state) => {
      state.loadingCount++;
    },
    endLoading: (state) => {
      if (state.loadingCount > 1)
        state.loadingCount--;
    }
  },
});

export const { startLoading, endLoading, isLoading, count } = loadingSlice.actions;
export default loadingSlice.reducer;