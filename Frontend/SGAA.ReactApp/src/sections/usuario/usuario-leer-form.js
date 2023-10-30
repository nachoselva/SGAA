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
            fullWidth
            label="Nombre"
            name="nombre"
            value={usuario.nombre}
            InputProps={{
              readOnly: true,
            }}
            disabled
          />
          <TextField
            fullWidth
            label="Apellido"
            name="apellido"
            value={usuario.apellido}
            InputProps={{
              readOnly: true,
            }}
            disabled
          />
          <TextField
            fullWidth
            label="Email"
            name="email"
            type="email"
            value={usuario.email}
            InputProps={{
              readOnly: true,
            }}
            disabled
          />
          <TextField
            fullWidth
            label="Rol"
            name="rol"
            select
            SelectProps={{ native: true }}
            value={usuario.roles}
            InputProps={{
              readOnly: true,
            }}
            disabled
          >
            <option key={'Inquilino'} value={'Inquilino'}>Inquilino</option>
            <option key={'Propietario'} value={'Propietario'}>Propietario</option>
            <option key={'Administrador'} value={'Administrador'}>Administrador</option>
          </TextField>
        </Stack>
      </form>
    </Box>
  );
};
