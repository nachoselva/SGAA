import { Box, Button, Grid, TextField, Typography } from '@mui/material';
import Moment from 'moment';
import { useRouter } from 'next/router';
import React, { useState } from 'react';
import { aprobarUnidad, rechazarUnidad } from '/src/api/administrador';
import { FancyFilePicker } from '/src/components/fancy-file-picker';

export const UnidadLeerForm = (props) => {
  const { unidad, rol } = props;
  const [nuevoComentario, setNuevoComentario] = useState(null);
  const [nuevoComentarioError, setNuevoComentarioError] = useState({});
  const router = useRouter();

  const onUnidadRechazada = (unidadId, comentario) => {
    if (comentario && comentario.trim()) {
      rechazarUnidad(unidadId, { comentario: comentario })
        .then(() => router.push('/administrador/unidad'));
    } else {
      const error = { ...nuevoComentarioError };
      error.error = true;
      error.helperText = "Comentario es obligatorio";
      setNuevoComentarioError(error);
    }
  }

  const onUnidadAprobada = (unidadId) => {
    aprobarUnidad(unidadId)
      .then(() => router.push('/administrador/unidad'));
  }

  return (
    <Box>
      <Grid container spacing={3} >
        <Grid item xs={12} sm={6}>
          <TextField
            variant="filled"
            fullWidth
            name="provinciaId"
            value={unidad.provincia}
            variant="filled"
            label={'Provincia'}
            InputProps={{
              readOnly: true,
            }}
          />
        </Grid>

        <Grid item xs={12} sm={6}>
          <TextField
            variant="filled"
            fullWidth
            name="ciudadId"
            value={unidad.ciudad}
            variant="filled"
            label={'Ciudad'}
            InputProps={{
              readOnly: true,
            }}
          />
        </Grid>
        <Grid item xs={12} sm={6}>
          <TextField
            variant="filled"
            fullWidth
            label="Calle"
            name="calle"
            value={unidad.calle}
            InputProps={{
              readOnly: true,
            }}
          />
        </Grid>

        <Grid item xs={12} sm={6}>
          <TextField
            variant="filled"
            type="number"
            fullWidth
            label="Altura"
            name="altura"
            value={unidad.altura}
            InputProps={{
              readOnly: true,
            }}
          />
        </Grid>

        <Grid item xs={12} sm={6}>
          <TextField
            variant="filled"
            fullWidth
            label="Piso"
            name="piso"
            value={unidad.piso}
            InputProps={{
              readOnly: true,
            }}
          />
        </Grid>

        <Grid item xs={12} sm={6}>
          <TextField
            variant="filled"
            fullWidth
            label="Departamento"
            name="departamento"
            value={unidad.departamento}
            InputProps={{
              readOnly: true,
            }}
          />
        </Grid>

        <Grid item xs={12} sm={6}>
          <TextField
            variant="filled"
            fullWidth
            label="Departamento"
            name="departamento"
            value={Moment(unidad.fechaAdquisicion).format('DD/MM/yyyy')}
            InputProps={{
              readOnly: true,
            }}
          />
        </Grid>

        <Grid item xs={12} sm={6}>
          <FancyFilePicker
            label="Título de propiedad"
            name="tituloPropiedadArchivo"
            file={unidad.tituloPropiedadArchivo}
            readOnly={true}
          />
        </Grid>

        <Grid item xs={12}>
          <Typography variant="h5">
            Detalle
          </Typography>
        </Grid>

        <Grid item xs={12} sm={6}>
          <TextField
            variant="filled"
            fullWidth
            label="Superficie"
            name="detalle.superficie"
            value={unidad.detalle.superficie}
            type="number"
            InputProps={{
              readOnly: true,
            }}
          />
        </Grid>

        <Grid item xs={12} sm={6}>
          <TextField
            variant="filled"
            fullWidth
            label="Ambientes"
            name="detalle.ambientes"
            value={unidad.detalle.ambientes}
            type="number"
            InputProps={{
              readOnly: true,
            }}
          />
        </Grid>

        <Grid item xs={12} sm={6}>
          <TextField
            variant="filled"
            fullWidth
            label="Baños"
            name="detalle.banios"
            value={unidad.detalle.banios}
            type="number"
            InputProps={{
              readOnly: true,
            }}
          />
        </Grid>

        <Grid item xs={12} sm={6}>
          <TextField
            variant="filled"
            fullWidth
            label="Dormitorios"
            name="detalle.dormitorios"
            value={unidad.detalle.dormitorios}
            type="number"
            InputProps={{
              readOnly: true,
            }}
          />
        </Grid>

        <Grid item xs={12} sm={6}>
          <TextField
            variant="filled"
            fullWidth
            label="Cocheras"
            name="detalle.cocheras"
            value={unidad.detalle.cocheras}
            type="number"
            InputProps={{
              readOnly: true,
            }}
          />
        </Grid>

        <Grid item xs={12}>
          <TextField
            variant="filled"
            multiline
            rows={5}
            fullWidth
            label="Descripción"
            name="detalle.descripcion"
            value={unidad.detalle.descripcion}
            InputProps={{
              readOnly: true,
            }}
          />
        </Grid>
        <Grid item xs={12}>
          <Typography variant="h5">
            Titulares
          </Typography>
        </Grid>
        {
          unidad.titulares.map((titular, index) =>
          (
            <Grid item xs={12} key={index}>
              <Box sx={{
                border: 1, borderRadius: '8px', 'borderStyle': 'solid', 'borderWidth': '1px', 'borderColor': '#1C2536', p: 2, mt: 1
              }} >
                <Grid container spacing={3}>
                  <Grid item xs={12} sm={6}>
                    <TextField
                      variant="filled"
                      fullWidth
                      label="Nombre"
                      name={"titulares[" + index + "].nombre"}
                      value={titular.nombre}
                      InputProps={{
                        readOnly: true,
                      }}
                    />
                  </Grid>
                  <Grid item xs={12} sm={6}>
                    <TextField
                      variant="filled"
                      fullWidth
                      label="Apellido"
                      name={"titulares[" + index + "].apellido"}
                      value={titular.apellido}
                      InputProps={{
                        readOnly: true,
                      }}
                    />
                  </Grid>
                  <Grid item xs={12} sm={6}>
                    <TextField
                      variant="filled"
                      fullWidth
                      label="Email"
                      name={"titulares[" + index + "].email"}
                      type="email"
                      value={titular.email}
                      InputProps={{
                        readOnly: true,
                      }}
                    />
                  </Grid>
                  <Grid item xs={12} sm={6}>
                    <TextField
                      variant="filled"
                      fullWidth
                      label="DNI"
                      name={"titulares[" + index + "].numeroIdentificacion"}
                      value={titular.numeroIdentificacion}
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
                      name={"titulares[" + index + "].domicilio"}
                      value={titular.domicilio}
                      InputProps={{
                        readOnly: true,
                      }}
                    />
                  </Grid>
                  <Grid item xs={12} sm={6}>
                    <TextField
                      variant="filled"
                      fullWidth
                      label="Fecha De Nacimiento"
                      name={"titulares[" + index + "].fechaNacimiento"}
                      value={Moment(titular.fechaNacimiento).format('DD/MM/yyyy')}
                      InputProps={{
                        readOnly: true,
                      }}
                    />
                  </Grid>
                  <Grid item xs={12} sm={6}>
                    <FancyFilePicker
                      label="Frente DNI"
                      name={"titulares[" + index + "].frenteIdentificacionArchivo"}
                      file={unidad.titulares[index].frenteIdentificacionArchivo}
                      readOnly={true}
                    />
                  </Grid>
                  <Grid item xs={12} sm={6}>
                    <FancyFilePicker
                      label="Dorso DNI"
                      name={"titulares[" + index + "].dorsoIdentificacionArchivo"}
                      file={unidad.titulares[index].dorsoIdentificacionArchivo}
                      readOnly={true}
                    />
                  </Grid>
                </Grid>
              </Box>
            </Grid>)
          )

        }

        <Grid item xs={12}>
          <Typography variant="h5">
            Comentarios
          </Typography>
        </Grid>
        {
          unidad.comentarios.length > 1 &&
          <Grid item xs={12}>
            <ul>
              {unidad.comentarios.sort((comentario) => comentario.fecha).map((com, index) =>
                <li key={index}>[{com.fecha}] : {com.comentario}</li>
              )}
            </ul>
          </Grid>
        }
        {
          rol == 'Administrador'
          &&
          <>
            <Grid item xs={12}>
              <TextField
                variant="filled"
                multiline
                rows={5}
                fullWidth
                label="Comentario Rechazo"
                name="nuevoComentario"
                value={nuevoComentario}
                onChange={(e) => setNuevoComentario(e.target.value)}
                error={nuevoComentarioError.error}
                helperText={nuevoComentarioError.helperText}
              />
            </Grid>

            <Grid item xs={12} sm={6}>
              <Button
                fullWidth
                size="large"
                sx={{ mt: 3 }}
                onClick={
                  () => onUnidadRechazada(unidad.id, nuevoComentario)
                }
                variant="contained"
              >
                Rechazar
              </Button>
            </Grid>

            <Grid item xs={12} sm={6}>
              <Button
                fullWidth
                size="large"
                sx={{ mt: 3 }}
                onClick={
                  () => onUnidadAprobada(unidad.id)
                }
                variant="contained"
              >
                Aprobar
              </Button>
            </Grid>
          </>
        }
      </Grid>

    </Box>
  );
};
