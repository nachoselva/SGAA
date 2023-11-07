import { Box, Button, Stack, TextField } from '@mui/material';
import Moment from 'moment';
import React from 'react';
import { crearPostulacion } from '/src/api/inquilino';
import { useRouter } from 'next/router';

export const PublicacionActivaForm = (props) => {
  const { publicacion } = props;
  const router = useRouter();

  const onPostulacion = () => {
    crearPostulacion({ publicacionId: publicacion.id })
      .then(() => router.push('/inquilino/postulacion'));
  }

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
            value={Moment(publicacion.inicioAlquiler).format('DD/MM/yyyy')}
            InputProps={{
              readOnly: true,
            }}
          />
          {
            publicacion.canUsuarioPostular &&
            <Button
              fullWidth
              size="large"
              sx={{ mt: 3 }}
              onClick={
                () => onPostulacion(publicacion.Id)
              }
              variant="contained"
            >
              Postular
            </Button>
          }
      </Stack>
    </form>
    </Box >
  );
};
