import { Box, Stack, TextField } from '@mui/material';
import React from 'react';
import { contratoStatus } from '/src/utils/status-labels';

export const ContratoLeerForm = (props) => {
  const { contrato } = props;

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
            value={contrato.id}
            InputProps={{
              readOnly: true,
            }}
          />
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
          <TextField
            variant="filled"
            fullWidth
            label="Hasta"
            name="fechaHasta"
            type="fechaHasta"
            value={contratoStatus[contrato.fechaHasta]}
            InputProps={{
              readOnly: true,
            }}
          />
          <TextField
            variant="filled"
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
        </Stack>
      </form>
    </Box>
  );
};
