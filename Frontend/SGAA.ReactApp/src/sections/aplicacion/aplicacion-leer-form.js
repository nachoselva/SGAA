import { Box, Button, Grid, TextField, Typography } from '@mui/material';
import Moment from 'moment';
import { useRouter } from 'next/router';
import React, { useState } from 'react';
import { aprobarAplicacion, rechazarAplicacion } from '/src/api/administrador';
import { FancyFilePicker } from '/src/components/fancy-file-picker';

export const AplicacionLeerForm = (props) => {
  const { aplicacion, rol } = props;
  const [nuevoComentario, setNuevoComentario] = useState('');
  const [nuevoComentarioError, setNuevoComentarioError] = useState({});
  const router = useRouter();
  const [puntuaciones, setPuntuaciones] = useState(aplicacion.postulantes
    .map(p => { return { postulanteId: p.id, error: false, helperText: null }; })
  );

  const onAplicacionRechazada = (aplicacionId, comentario) => {
    if (comentario && comentario.trim()) {
      rechazarAplicacion(aplicacionId, { comentario: comentario })
        .then(() => router.push('/administrador/aplicacion'));
    } else {
      const error = { ...nuevoComentarioError };
      error.error = true;
      error.helperText = "Comentario es obligatorio";
      setNuevoComentarioError(error);
    }
  }

  const onAplicacionAprobada = (aplicacionId) => {
    let hasError = false;
    const list = [...puntuaciones];
    for (var i = 0; i < list.length; i++) {
      if (!list[i].puntuacionCrediticia) {
        hasError = true;
        list[i].puntuacionCrediticiaError = true
        list[i].puntuacionCrediticiaHelperText = "Puntuación crediticia es obligatoria";
      }
      else if (list[i].puntuacionCrediticia > 1000 || puntuaciones[i].puntuacionCrediticia < 0) {
        hasError = true;
        list[i].puntuacionCrediticiaError = true
        list[i].puntuacionCrediticiaHelperText = "Puntuación crediticia debe ser entre 0 y 1000";
      }

      if (!list[i].puntuacionPenal) {
        hasError = true;
        list[i].puntuacionPenalError = true;
        list[i].puntuacionPenalHelperText = "Puntuación penal es obligatoria";
      }
      else if (list[i].puntuacionPenal > 1000 || list[i].puntuacionPenal < 0) {
        hasError = true;
        list[i].puntuacionPenalError = true;
        list[i].puntuacionPenalHelperText = "Puntuación penal debe ser entre 0 y 1000";
      }
    }
    if (!hasError) {
      aprobarAplicacion(aplicacionId, { puntuaciones: puntuaciones })
        .then(() => router.push('/administrador/aplicacion'));
    }
    else {
      setPuntuaciones(list);
    }
  }

  return (
    <Box>
      <Grid container spacing={3} >
        <Grid item xs={12}>
          <Typography variant="h5">
            Postulantes
          </Typography>
        </Grid>
        {
          puntuaciones.map((puntuacion, index) => {
            const postulante = aplicacion.postulantes[index];
            return (<Grid item xs={12}>
              <Box sx={{
                border: 1, borderRadius: '8px', 'borderStyle': 'solid', 'borderWidth': '1px', 'borderColor': '#1C2536', p: 2, mt: 1
              }} >
                <Grid container spacing={3}>
                  <Grid item xs={12} sm={6}>
                    <TextField
                      variant="filled"
                      fullWidth
                      label="Nombre"
                      name={"postulantes[" + index + "].nombre"}
                      value={postulante.nombre}
                    />
                  </Grid>
                  <Grid item xs={12} sm={6}>
                    <TextField
                      variant="filled"
                      fullWidth
                      label="Apellido"
                      name={"postulantes[" + index + "].apellido"}
                      value={postulante.apellido}
                    />
                  </Grid>
                  <Grid item xs={12} sm={6}>
                    <TextField
                      variant="filled"
                      fullWidth
                      label="Email"
                      name={"postulantes[" + index + "].email"}
                      value={postulante.email}
                      type="email"
                    />
                  </Grid>
                  <Grid item xs={12} sm={6}>
                    <TextField
                      variant="filled"
                      fullWidth
                      label="DNI"
                      name={"postulantes[" + index + "].numeroIdentificacion"}
                      value={postulante.numeroIdentificacion}
                    />
                  </Grid>
                  <Grid item xs={12} sm={6}>
                    <TextField
                      variant="filled"
                      fullWidth
                      label="Domicilio"
                      name={"postulantes[" + index + "].domicilio"}
                      value={postulante.domicilio}
                    />
                  </Grid>
                  <Grid item xs={12} sm={6}>
                    <TextField
                      variant="filled"
                      fullWidth
                      label="Fecha De Nacimiento"
                      name={"postulantes[" + index + "].fechaNacimiento"}
                      value={postulante.fechaNacimiento && Moment(postulante.fechaNacimiento).format('DD/MM/yyyy')}
                      InputProps={{
                        readOnly: true,
                      }}
                    />
                  </Grid>
                  <Grid item xs={12} sm={6}>
                    <FancyFilePicker
                      label="Frente DNI"
                      name={"postulantes[" + index + "].frenteIdentificacionArchivo"}
                      file={postulante.frenteIdentificacionArchivo}
                      readOnly={true}
                    />
                  </Grid>
                  <Grid item xs={12} sm={6}>
                    <FancyFilePicker
                      label="Dorso DNI"
                      name={"postulantes[" + index + "].dorsoIdentificacionArchivo"}
                      file={postulante.dorsoIdentificacionArchivo}
                      readOnly={true}
                    />
                  </Grid>
                  <Grid item xs={12} sm={6}>
                    <TextField
                      variant="filled"
                      fullWidth
                      label="Nombre Empresa"
                      name={"postulantes[" + index + "].nombreEmpresa"}
                      value={postulante.nombreEmpresa}
                      InputProps={{
                        readOnly: true,
                      }}
                    />
                  </Grid>
                  <Grid item xs={12} sm={6}>
                    <TextField
                      variant="filled"
                      fullWidth
                      label="Fecha de Empleo"
                      name={"postulantes[" + index + "].fechaEmpleadoDesde"}
                      value={postulante.fechaEmpleadoDesde && Moment(postulante.fechaEmpleadoDesde).format('DD/MM/yyyy')}
                      InputProps={{
                        readOnly: true,
                      }}
                    />
                  </Grid>
                  <Grid item xs={12} sm={6}>
                    <TextField
                      variant="filled"
                      fullWidth
                      label="Ingreso Mensual"
                      name={"postulantes[" + index + "].ingresoMensual"}
                      value={postulante.ingresoMensual}
                      type="number"
                      InputProps={{
                        readOnly: true,
                      }}
                    />
                  </Grid>
                  <Grid item xs={12} sm={6}>
                    <FancyFilePicker
                      label="Recibo de sueldo"
                      name={"postulantes[" + index + "].reciboDeSueldoArchivo"}
                      file={postulante.reciboDeSueldoArchivo}
                      readOnly={true}
                    />
                  </Grid>
                  <Grid item xs={12} sm={6} >
                    <TextField
                      variant="filled"
                      error={puntuacion.puntuacionCrediticiaError}
                      helperText={puntuacion.puntuacionCrediticiaHelperText}
                      fullWidth
                      label="Puntuación Crediticia"
                      name={"puntuaciones[" + index + "].puntuacionCrediticia"}
                      value={postulante.puntuacionCrediticia ?? puntuacion.puntuacionCrediticia}
                      onChange={(e) => { puntuaciones[index].puntuacionCrediticia = e.target.value; setPuntuaciones(puntuaciones) }}
                      type='number'
                      InputProps={{
                        readOnly: rol != 'Administrador' || aplicacion.status != 'AprobacionPendiente'
                      }}
                    />
                  </Grid>
                  <Grid item xs={12} sm={6}>
                    <TextField
                      variant="filled"
                      error={puntuacion.puntuacionPenalError}
                      helperText={puntuacion.puntuacionPenalHelperText}
                      fullWidth
                      label="Puntuación Penal"
                      name={"puntuaciones[" + index + "].puntuacionPenal"}
                      onChange={(e) => { puntuaciones[index].puntuacionPenal = e.target.value; setPuntuaciones(puntuaciones) }}
                      value={postulante.puntuacionPenal ?? puntuacion.puntuacionPenal}
                      type='number'
                      InputProps={{
                        readOnly: rol != 'Administrador' || aplicacion.status != 'AprobacionPendiente'
                      }}
                    />
                  </Grid>
                </Grid>
              </Box>
            </Grid>);

          }

          )
        }
        <Grid item xs={12}>
          <Typography variant="h5">
            Garantías
          </Typography>
        </Grid>
        {
          aplicacion.garantias.map((garantia, index) =>
          (<Grid item xs={12}>
            <Box sx={{
              border: 1, borderRadius: '8px', 'borderStyle': 'solid', 'borderWidth': '1px', 'borderColor': '#1C2536', p: 2, mt: 1
            }} >
              <Grid container spacing={3}>
                <Grid item xs={12} sm={6}>
                  <TextField
                    variant="filled"
                    fullWidth
                    label="Monto"
                    name={"garantias[" + index + "].monto"}
                    value={garantia.monto}
                    type="number"
                    InputProps={{
                      readOnly: true,
                    }}
                  />
                </Grid>
                <Grid item xs={12} sm={6}>
                  <FancyFilePicker
                    label="Escritura"
                    name={"postulantes[" + index + "].archivo"}
                    file={garantia.archivo}
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
          aplicacion.comentarios.length > 0 &&
          <Grid item xs={12}>
            <ul>
              {props.aplicacion.comentarios.sort((comentario) => comentario.fecha).map(com =>
                <li>[{Moment(com.fecha).format('DD/MM/yyyy hh:mm:ss')}] : {com.comentario}</li>
              )}
            </ul>
          </Grid>
        }
        {
          aplicacion.comentarios.length == 0 &&
          <Grid item xs={12}>
            <p>Aún no hay comentarios registrados</p>
          </Grid>
        }
        {
          aplicacion.status == 'AprobacionPendiente' &&
          rol == 'Administrador' &&
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
                  () => onAplicacionRechazada(aplicacion.id, nuevoComentario)
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
                  () => onAplicacionAprobada(aplicacion.id, puntuaciones)
                }
                variant="contained"
              >
                Aprobar
              </Button>
            </Grid>
          </>
        }
      </Grid>
    </Box >
  );
};
