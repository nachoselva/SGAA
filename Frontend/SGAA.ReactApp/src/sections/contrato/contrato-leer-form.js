import { Box, Button, Grid, TextField } from '@mui/material';
import { useRouter } from 'next/router';
import React from 'react';
import { firmarContrato } from '/src/api/common';
import { FancyFilePicker } from '/src/components/fancy-file-picker';

export const ContratoLeerForm = (props) => {
  const { contrato } = props;
  const router = useRouter();

  const onFirmarContrato = (contratoId) => {
    firmarContrato(contratoId)
      .then(() => router.push('/contrato'));
  }

  return (
    <Box>
      <Grid container spacing={3}>
        <Grid item xs={12} sm={6}>
          <TextField
            variant="filled"
            fullWidth
            label="Id"
            name="id"
            value={contrato.id}
            InputProps={{
              readOnly: true,
            }}
          />
        </Grid>
        <Grid item xs={12} sm={6}>
          <TextField
            variant="filled"
            fullWidth
            label="Domicilio"
            name="domicilio"
            value={contrato.domicilio}
            InputProps={{
              readOnly: true,
            }}
          />
        </Grid>
        <Grid item xs={12} sm={6}>
          <TextField
            variant="filled"
            fullWidth
            label="Desde"
            name="fechaDesde"
            value={contrato.fechaDesde}
            InputProps={{
              readOnly: true,
            }}
          />
        </Grid>
        <Grid item xs={12} sm={6}>
          <TextField
            variant="filled"
            fullWidth
            label="Hasta"
            name="fechaHasta"
            type="fechaHasta"
            value={contrato.fechaHasta}
            InputProps={{
              readOnly: true,
            }}
          />
        </Grid>
        <Grid item xs={12} sm={6}>
          <TextField
            variant="filled"
            fullWidth
            label="Estado"
            name="status"
            type="status"
            value={contrato.status}
            InputProps={{
              readOnly: true,
            }}
          />
        </Grid>
        <Grid item xs={12} sm={6}>
          <TextField
            variant="filled"
            fullWidth
            label="Orden Renovacion"
            name="ordenRenovacion"
            type="ordenRenovacion"
            value={contrato.ordenRenovacion}
            InputProps={{
              readOnly: true,
            }}
          />
        </Grid>
        <Grid item xs={12} sm={6}>
          <TextField
            variant="filled"
            fullWidth
            label="Inquilinos"
            name="inquilinos"
            type="inquilinos"
            value={contrato.inquilinos}
            InputProps={{
              readOnly: true,
            }}
          />
        </Grid>
        <Grid item xs={12} sm={6}>
          <TextField
            variant="filled"
            fullWidth
            label="Propietarios"
            name="propietarios"
            type="propietarios"
            value={contrato.propietarios}
            InputProps={{
              readOnly: true,
            }}
          />
        </Grid>
        <Grid item xs={12}>
          <FancyFilePicker
            label="Contrato"
            name="contrato"
            file={contrato.archivo}
            readOnly={true}
          />
        </Grid>
        {
          contrato.canUsuarioFirmar &&
          <Grid item xs={12} sm={6}>
            <Button
              fullWidth
              size="large"
              sx={{ mt: 3 }}
              onClick={
                () => onFirmarContrato(contrato.id)
              }
              variant="contained"
            >
              Firmar
            </Button>
          </Grid>
        }
      </Grid>
    </Box>
  );
};
