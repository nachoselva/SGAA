import { Box, Button, Stack, TextField, Grid, Typography } from '@mui/material';
import Moment from 'moment';
import { useRouter } from 'next/router';
import React from 'react';
import Carousel from 'react-material-ui-carousel';
import { crearPostulacion } from '/src/api/inquilino';

export const PublicacionActivaForm = (props) => {
  const { publicacion } = props;
  const router = useRouter();

  const onPostulacion = () => {
    crearPostulacion({ publicacionId: publicacion.id })
      .then(() => router.push('/inquilino/postulacion'));
  }

  return (
    <Box>
      <Grid container spacing={3} >
        <Grid item xs={12}>
          <Typography variant="h5">
            Publicación
          </Typography>
        </Grid>
        <Grid item xs={12} sm={6}>
          <TextField
            variant="filled"
            fullWidth
            label="Domicilio"
            name="domicilioCompleto"
            value={publicacion.domicilioCompleto}
            InputProps={{
              readOnly: true
            }}
          />
        </Grid>
        <Grid item xs={12} sm={6}>
          <TextField
            variant="filled"
            fullWidth
            label="Monto Alquiler"
            name="montoAlquiler"
            value={publicacion.montoAlquiler}
            InputProps={{
              readOnly: true
            }}
          />
        </Grid>
        <Grid item xs={12} sm={6}>
          <TextField
            variant="filled"
            fullWidth
            label="Disponible desde"
            name="inicioAlquiler"
            type="inicioAlquiler"
            value={Moment(publicacion.inicioAlquiler).format('DD/MM/yyyy')}
            InputProps={{
              readOnly: true
            }}
          />
        </Grid>
        <Grid item xs={12} sm={6}>
          <TextField
            variant="filled"
            fullWidth
            label="Superficie"
            name="superficie"
            value={publicacion.unidad.detalle.superficie}
            InputProps={{
              readOnly: true
            }}
          />
        </Grid>
        <Grid item xs={12} sm={6}>
          <TextField
            variant="filled"
            fullWidth
            label="Ambientes"
            name="ambientes"
            value={publicacion.unidad.detalle.ambientes}
            InputProps={{
              readOnly: true
            }}
          />
        </Grid>
        <Grid item xs={12} sm={6}>
          <TextField
            variant="filled"
            fullWidth
            label="Baños"
            name="banios"
            value={publicacion.unidad.detalle.banios}
            InputProps={{
              readOnly: true
            }}
          />
        </Grid>
        <Grid item xs={12} sm={6}>
          <TextField
            variant="filled"
            fullWidth
            label="Dormitorios"
            name="dormitorios"
            value={publicacion.unidad.detalle.dormitorios}
            InputProps={{
              readOnly: true
            }}
          />
        </Grid>
        <Grid item xs={12} sm={6}>
          <TextField
            variant="filled"
            fullWidth
            label="Cocheras"
            name="cocheras"
            value={publicacion.unidad.detalle.cocheras}
            InputProps={{
              readOnly: true
            }}
          />
        </Grid>
        <Grid item xs={12}>
          <TextField
            multiline
            rows={5}
            variant="filled"
            fullWidth
            label="Descripcion"
            name="descripcion"
            value={publicacion.unidad.detalle.descripcion}
            InputProps={{
              readOnly: true
            }}
          />
        </Grid>
        {
          publicacion.unidad.detalle.imagenes.length > 0 &&
          <>
            <Grid item xs={12}>
              <Typography variant="h5">
                Imagenes
              </Typography>
            </Grid>
            <Grid item xs={12}>
              <Carousel >
                {
                  publicacion.unidad.detalle.imagenes.map((img, index) => {
                    return (
                      <Box key={index} component='div' sx={{
                        border: 1,
                        borderRadius: '8px',
                        borderStyle: 'solid',
                        borderWidth: '1px',
                        borderColor: '#1C2536',
                        p: '10px'
                      }}>
                        <p>{img.titulo}</p>
                        <Box component="div" sx={{
                          display: 'flex',
                          justifyContent: 'center'
                        }}>
                          <Box
                            component="img"
                            sx={{
                              height: '500px'
                            }}
                            src={JSON.parse(img.archivo).base64}
                          />
                        </Box>
                        <p>{img.descripcion}</p>
                      </Box>);
                  }
                  )
                }
              </Carousel>

            </Grid>
          </>
        }
        {
          publicacion.canUsuarioPostular &&
          <Grid item xs={12}>
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
          </Grid>
        }
      </Grid>
    </Box >
  );
};
