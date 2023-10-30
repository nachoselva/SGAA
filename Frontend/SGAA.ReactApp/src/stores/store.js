import { configureStore } from "@reduxjs/toolkit";
import usuarioSlice from "/src/slices/usuario-slice";
import loadingSlice from "/src/slices/loading-slice";

export const store = configureStore({
  reducer: {
    usuario: usuarioSlice,
    loading: loadingSlice
  }
});