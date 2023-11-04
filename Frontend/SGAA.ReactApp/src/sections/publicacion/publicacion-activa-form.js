import { Box, Stack, TextField } from '@mui/material';
import React from 'react';

export const PublicacionActivaForm = (props) => {
  const { publicacion } = props;

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
            value={publicacion.id}
            InputProps={{
              readOnly: true,
            }}
          />
          <TextField
            variant="filled"
            fullWidth
            label="Domicilio"
            name="domicilioCompleto"
            value={publicacion.domicilioCompleto}
            InputProps={{
              readOnly: true,
            }}
          />
          <TextField
            variant="filled"
            fullWidth
            label="Monto Alquiler"
            name="montoAlquiler"
            value={publicacion.montoAlquiler}
            InputProps={{
              readOnly: true,
            }}
          />
          <TextField
            variant="filled"
            fullWidth
            label="Inicio"
            name="inicioAlquiler"
            type="inicioAlquiler"
            value={publicacion.inicioAlquiler}
            InputProps={{
              readOnly: true,
            }}
          />
        </Stack>
      </form>
    </Box>
  );
};
