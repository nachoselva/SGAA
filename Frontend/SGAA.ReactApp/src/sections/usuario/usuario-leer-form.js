import { Box, Stack, TextField } from '@mui/material';
import React from 'react';

export const UsuarioLeerForm = (props) => {
  const { usuario } = props;

  return (
    <Box>
      <form
        noValidate
      >
        <Stack spacing={3}>
          <TextField
            variant="filled"
            fullWidth
            label="Id"
            name="id"
            value={usuario.id}
            InputProps={{
              readOnly: true,
            }}
          />
          <TextField
            variant="filled"
            fullWidth
            label="Nombre"
            name="nombre"
            value={usuario.nombre}
            InputProps={{
              readOnly: true,
            }}
          />
          <TextField
            variant="filled"
            fullWidth
            label="Apellido"
            name="apellido"
            value={usuario.apellido}
            InputProps={{
              readOnly: true,
            }}
          />
          <TextField
            variant="filled"
            fullWidth
            label="Email"
            name="email"
            type="email"
            value={usuario.email}
            InputProps={{
              readOnly: true,
            }}
          />
          <TextField
            variant="filled"
            fullWidth
            label="Rol"
            name="rol"
            value={usuario.roles}
            InputProps={{
              readOnly: true,
            }}
          />
        </Stack>
      </form>
    </Box>
  );
};
