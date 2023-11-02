import { Box, Stack, TextField } from '@mui/material';
import React from 'react';

export const AplicacionLeerForm = (props) => {
  const { aplicacion } = props;

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
            value={aplicacion.id}
            InputProps={{
              readOnly: true,
            }}
          />
          <TextField
            variant="filled"
            fullWidth
            label="Inquilino"
            name="inquilinoUsuarioNombreCompleto"
            value={aplicacion.inquilinoUsuarioNombreCompleto}
            InputProps={{
              readOnly: true,
            }}
          />
          <TextField
            variant="filled"
            fullWidth
            label="Estado"
            name="status"
            type="status"
            value={aplicacion.status}
            InputProps={{
              readOnly: true,
            }}
          />
          <TextField
            variant="filled"
            fullWidth
            label="Puntuación"
            name="puntuacionTotal"
            type="puntuacionTotal"
            value={aplicacion.puntuacionTotal}
            InputProps={{
              readOnly: true,
            }}
          />
          <TextField
            variant="filled"
            fullWidth
            label="Postulaciones"
            name="postulaciones"
            type="postulaciones"
            value={aplicacion.postulaciones}
            InputProps={{
              readOnly: true,
            }}
          />
        </Stack>
      </form>
    </Box>
  );
};
