import { Box, Button, Grid, TextField } from '@mui/material';
import { useRouter } from 'next/router';
import React from 'react';
import { aprobarPago } from '/src/api/propietario';
import { FancyFilePicker } from '/src/components/fancy-file-picker';

export const PagoLeerForm = (props) => {
  const { pago, rol } = props;
  const router = useRouter();

  const onPagoAprobado = (pagoId) => {
    aprobarPago(pagoId)
      .then(() => router.push('/propietario/pago'));
  }

  return (
    <Box>
      <Grid container spacing={3} >
        <Grid item xs={12}>
          <TextField
            variant="filled"
            fullWidth
            label="Estado"
            name={"status"}
            value={pago.status}
            InputProps={{
              readOnly: true
            }}
          />
        </Grid>
        <Grid item xs={12}>
          <FancyFilePicker
            label="Comprobante de Pago"
            name={"archivo"}
            file={pago.archivo}
            readOnly={true}
          />
        </Grid>
        {
          pago.status == 'Abonado' &&
          rol == 'Propietario' &&
          <Grid item xs={12} sm={6}>
            <Button
              fullWidth
              size="large"
              sx={{ mt: 3 }}
              onClick={
                () => onPagoAprobado(pago.id)
              }
              variant="contained"
            >
              Aprobar
            </Button>
          </Grid>
        }
      </Grid>

    </Box >
  );
};
