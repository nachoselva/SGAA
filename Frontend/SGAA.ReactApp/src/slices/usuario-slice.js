import { createSlice } from "@reduxjs/toolkit";

const initialState = {
  nombre : '',
  apellido : '',
  email : '',
  roles : ''
};

export const usuarioSlice = createSlice({
  name: "usuario",
  initialState,
  reducers: {
    addUsuario: (state, action) => {
      const { nombre, apellido, email, roles } = action.payload;
      state.nombre = nombre;
      state.apellido = apellido;
      state.email = email;
      state.roles = roles;
    }
  },
});

export const { addUsuario } = usuarioSlice.actions;
export default usuarioSlice.reducer;