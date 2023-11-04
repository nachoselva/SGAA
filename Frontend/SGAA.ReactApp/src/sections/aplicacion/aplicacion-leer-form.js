import { Box, Button, Grid, TextField, Typography } from '@mui/material';
import { useRouter } from 'next/router';
import React, { useState } from 'react';
import { FancyFilePicker } from '/src/components/fancy-file-picker';
import { aprobarAplicacion, rechazarAplicacion } from '/src/api/administrador';

export const AplicacionLeerForm = (props) => {
  const { aplicacion, rol } = props;
  const [nuevoComentario, setNuevoComentario] = useState(null);
  const router = useRouter();
  const [puntuaciones, setPuntuaciones] = useState(aplicacion.postulantes
    .map(p => { return { nombre: p.nombre + ' ' + p.apellido, postulanteId: p.id }; })
  );

  const onAplicacionRechazada = (aplicacionId, comentario) => {
    rechazarAplicacion(aplicacionId, { comentario: comentario })
      .then(() => router.push('/administrador/aplicacion'));
  }

  const onAplicacionAprobada = (aplicacionId) => {
    aprobarAplicacion(aplicacionId, { puntuaciones: puntuaciones })
      .then(() => router.push('/administrador/aplicacion'));
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
          aplicacion.postulantes.map((postulante, index) =>
          (<Grid item xs={12}>
            <Box sx={{
              border: 1, borderRadius: '8px', 'border- style': 'solid', 'border-width': '1px', 'border-color': '#1C2536', p: 2, mt: 1
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
                    value={postulante.fechaNacimiento}
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
                    value={postulante.fechaEmpleadoDesde}
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
              </Grid>
            </Box>
          </Grid>)
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
              border: 1, borderRadius: '8px', 'border- style': 'solid', 'border-width': '1px', 'border-color': '#1C2536', p: 2, mt: 1
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
                    label="Archivo"
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
                <li>[{com.fecha}] : {com.comentario}</li>
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
              />
            </Grid>

            {
              puntuaciones.map((puntuacion, index) =>
              (<Grid item xs={12}>
                <Box sx={{
                  border: 1, borderRadius: '8px', 'border- style': 'solid', 'border-width': '1px', 'border-color': '#1C2536', p: 2, mt: 1
                }}>
                  <Grid container spacing={3}>
                    <Grid item xs={12} sx={{ mb: 1 }}>
                      {puntuacion.nombre}
                    </Grid>
                    <Grid item xs={12} sm={6} >
                      <TextField
                        variant="filled"
                        fullWidth
                        label="Puntuación Crediticia"
                        name={"puntuaciones[" + index + "].puntuacionCrediticia"}
                        value={puntuacion.puntuacionCrediticia}
                        onChange={(e) => { puntuaciones[index].puntuacionCrediticia = e.target.value; setPuntuaciones(puntuaciones) }}
                        type='number'
                      />
                    </Grid>
                    <Grid item xs={12} sm={6}>
                      <TextField
                        variant="filled"
                        fullWidth
                        label="Puntuación Penal"
                        name={"puntuaciones[" + index + "].puntuacionPenal"}
                        onChange={(e) => { puntuaciones[index].puntuacionPenal = e.target.value; setPuntuaciones(puntuaciones) }}
                        value={puntuacion.puntuacionPenal}
                        type='number'
                      />
                    </Grid>
                  </Grid>
                </Box>
              </Grid>))
            }

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
