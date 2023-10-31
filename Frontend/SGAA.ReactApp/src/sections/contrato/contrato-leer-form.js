import { Box, Stack, TextField } from '@mui/material';
import React from 'react';

export const ContratoLeerForm = (props) => {
  const { contrato } = props;

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
            value={contrato.id}
            InputProps={{
              readOnly: true,
            }}
          />
          <TextField
            fullWidth
            label="Domicilio"
            name="domicilio"
            value={contrato.domicilio}
            InputProps={{
              readOnly: true,
            }}
          />
          <TextField
            fullWidth
            label="Desde"
            name="fechaDesde"
            value={contrato.fechaDesde}
            InputProps={{
              readOnly: true,
            }}
          />
          <TextField
            fullWidth
            label="Hasta"
            name="fechaHasta"
            type="fechaHasta"
            value={contrato.fechaHasta}
            InputProps={{
              readOnly: true,
            }}
          />
          <TextField
            fullWidth
            label="Contrato"
            name="contrato"
            type="contrato"
            /*value={contrato.contrato}*/
            InputProps={{
              readOnly: true,
            }}
          />
          <TextField
            fullWidth
            label="Estado"
            name="status"
            type="status"
            value={contrato.status}
            InputProps={{
              readOnly: true,
            }}
          />
          <TextField
            fullWidth
            label="Orden Renovacion"
            name="ordenRenovacion"
            type="ordenRenovacion"
            value={contrato.ordenRenovacion}
            InputProps={{
              readOnly: true,
            }}
          />
          <TextField
            fullWidth
            label="Inquilinos"
            name="inquilinos"
            type="inquilinos"
            value={contrato.inquilinos}
            InputProps={{
              readOnly: true,
            }}
          />
          <TextField
            fullWidth
            label="Propietarios"
            name="propietarios"
            type="propietarios"
            value={contrato.propietarios}
            InputProps={{
              readOnly: true,
            }}
          />
        </Stack>
      </form>
    </Box>
  );
};
