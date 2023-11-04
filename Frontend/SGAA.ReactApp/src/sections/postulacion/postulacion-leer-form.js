import { Box, Stack, TextField } from '@mui/material';
import React from 'react';

export const PostulacionLeerForm = (props) => {
  const { postulacion } = props;

  return (
    <Box>
      <Stack spacing={3}>
        <TextField
          variant="filled"
          fullWidth
          label="Id"
          name="id"
          value={postulacion.id}
          InputProps={{
            readOnly: true,
          }}
        />
        <TextField
          variant="filled"
          fullWidth
          label="Domicilio"
          name="domicilioCompleto"
          value={postulacion.domicilioCompleto}
          InputProps={{
            readOnly: true,
          }}
        />
        <TextField
          variant="filled"
          fullWidth
          label="Monto Alquiler"
          name="montoAlquiler"
          value={postulacion.montoAlquiler}
          InputProps={{
            readOnly: true,
          }}
        />
        <TextField
          variant="filled"
          fullWidth
          label="Fecha Postulación"
          name="fechaPostulacion"
          type="fechaPostulacion"
          value={postulacion.fechaPostulacion}
          InputProps={{
            readOnly: true,
          }}
        />
        <TextField
          variant="filled"
          fullWidth
          label="Fecha Oferta"
          name="fechaOferta"
          type="fechaOferta"
          value={postulacion.fechaOferta}
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
          value={postulacion.status}
          InputProps={{
            readOnly: true,
          }}
        />
      </Stack>
    </Box>
  );
};
