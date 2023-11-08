import { Box, Stack, TextField } from '@mui/material';
import Moment from 'moment';
import React from 'react';
import { publicacionStatus } from '/src/utils/status-labels';

export const PublicacionLeerForm = (props) => {
  const { publicacion } = props;

  return (
    <Box>
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
          value={Moment(publicacion.inicioAlquiler).format('DD/MM/yyyy')}
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
          value={publicacion.postulaciones}
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
          value={publicacionStatus[publicacion.status]}
          InputProps={{
            readOnly: true,
          }}
        />
      </Stack>
    </Box>
  );
};
