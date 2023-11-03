import { Box, Grid, TextField, Typography } from '@mui/material';
import React from 'react';
import { FancyFilePicker } from '/src/components/fancy-file-picker';

export const AplicacionLeerForm = (props) => {

  return (
    <Box>
      <Grid container spacing={3} >
        <Grid item xs={12}>
          <Typography variant="h5">
            Postulantes
          </Typography>
        </Grid>
        {
          props.aplicacion.postulantes.map((postulante, index) =>
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
          props.aplicacion.garantias.map((garantia, index) =>
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
        {
          props.aplicacion?.comentarios?.length > 0 &&
          <Grid item xs={12}>
            <Typography variant="h5">
              Comentarios
            </Typography>
          </Grid>
        }
        {
          props.aplicacion?.comentarios?.length > 0 &&
          <Grid item xs={12}>
            <ul>
              {props.unidad.comentarios.sort((comentario) => comentario.fecha).map(com =>
                <li>[{com.fecha}] : {com.comentario}</li>
              )}
            </ul>
          </Grid>
        }
      </Grid>
    </Box >
  );
};
