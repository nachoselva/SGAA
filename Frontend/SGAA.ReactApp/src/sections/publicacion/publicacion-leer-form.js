import { Box, Stack, TextField } from '@mui/material';
import React from 'react';

export const PublicacionLeerForm = (props) => {
  const { publicacion } = props;

  return (
    <Box>
      <form
        noValidate
      >
        <Stack spacing={3}>
          <TextField
            fullWidth
            label="Id"
            name="id"
            value={publicacion.id}
            InputProps={{
              readOnly: true,
            }}
          />
          <TextField
            fullWidth
            label="Domicilio"
            name="domicilioCompleto"
            value={publicacion.domicilioCompleto}
            InputProps={{
              readOnly: true,
            }}
          />
          <TextField
            fullWidth
            label="Monto Alquiler"
            name="montoAlquiler"
            value={publicacion.montoAlquiler}
            InputProps={{
              readOnly: true,
            }}
          />
          <TextField
            fullWidth
            label="Inicio"
            name="inicioAlquiler"
            type="inicioAlquiler"
            value={publicacion.inicioAlquiler}
            InputProps={{
              readOnly: true,
            }}
          />
          <TextField
            fullWidth
            label="Postulaciones"
            name="postulaciones"
            type="postulaciones"
            value={publicacion.postulaciones}
            InputProps={{
              readOnly: true,
            }}
          />
          <TextField
            fullWidth
            label="Estado"
            name="status"
            type="status"
            value={publicacion.status}
            InputProps={{
              readOnly: true,
            }}
          />
        </Stack>
      </form>
    </Box>
  );
};
