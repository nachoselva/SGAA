import { Box, Stack, TextField } from '@mui/material';
import React from 'react';

export const UnidadLeerForm = (props) => {
  const { unidad } = props;

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
            value={unidad.id}
            InputProps={{
              readOnly: true,
            }}
          />
          <TextField
            fullWidth
            label="Domicilio"
            name="domicilioCompleto"
            value={unidad.domicilioCompleto}
            InputProps={{
              readOnly: true,
            }}
          />
          <TextField
            fullWidth
            label="Provincia"
            name="provincia"
            value={unidad.provincia}
            InputProps={{
              readOnly: true,
            }}
          />
          <TextField
            fullWidth
            label="Estado"
            name="status"
            type="status"
            value={unidad.status}
            InputProps={{
              readOnly: true,
            }}
          />
        </Stack>
      </form>
    </Box>
  );
};
