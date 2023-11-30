import { Box, Card, Container, Grid, Stack, Typography } from '@mui/material';
import Head from 'next/head';
import { FancyBreadcrumbs } from '/src/components/fancy-breadcrumbs';
import { Layout as DashboardLayout } from '/src/layouts/dashboard/layout';

const breadcrumbsConfig = [
  { url: '/inicio', title: 'Inicio' },
  { url: '/tyc', title: 'Términos y Condiciones' }
];

const Page = () => {
  return (
    <>
      <Head>
        <title>
          SGAA - Inicio
        </title>
      </Head>
      <FancyBreadcrumbs breadcrumbsConfig={breadcrumbsConfig}>
      </FancyBreadcrumbs>
      <Box
        component="main"
      >
        <Container maxWidth="xl">
          <Stack spacing={3}>
            <Stack
              direction="row"
              justifyContent="space-between"
              spacing={4}
            >
              <Stack spacing={1}>
                <Typography variant="h4">
                  Términos y Condiciones
                </Typography>
              </Stack>
            </Stack>
            <Grid
              container
              spacing={3}
            >
              <Grid
                xs={12}
              >
                <Card sx={{ border: 1, borderRadius: '8px', 'borderStyle': 'solid', 'borderWidth': '1px', 'borderColor': '#1C2536', p: 2, mt: 1 }} >
                  <Typography variant="h6">1. Uso del Sistema</Typography>
                  <p>El Sistema de Gestión de Alquileres Autónomos (SGAA) es una plataforma diseñada para facilitar la administración de alquileres y promover un proceso de selección de inquilinos justo y equitativo sin la intervención de administradores o inmobiliarias, fomentando los alquileres de tipo Dueño Directo. El uso del sistema es personal, y se prohíbe utilizarlo en nombre de otra persona a menos que se cuente con un poder legal para hacerlo.</p>

                  <Typography variant="h6">2. Prohibición de Explotación Comercial Inmobiliaria</Typography>
                  <p>Se prohíbe el uso del SGAA para la explotación comercial inmobiliaria. La plataforma está diseñada exclusivamente para facilitar relaciones directas entre propietarios e inquilinos, y no se permiten comisiones relacionadas a los contratos firmados utilizando SGAA.</p>

                  <Typography variant="h6">3. Confidencialidad de Datos</Typography>
                  <p>Los propietarios no podrán tener conocimiento de los potenciales inquilinos hasta que la reserva esté realizada. La privacidad de la información personal de los inquilinos se mantendrá de manera estricta.</p>

                  <Typography variant="h6">4. No Discriminación</Typography>
                  <p>Está estrictamente prohibido discriminar a los inquilinos por su nacionalidad, raza, género, edad u otras características personales. En SGAA, promovemos la igualdad y la no discriminación en la selección de inquilinos.</p>

                  <Typography variant="h6">5. Selección Equitativa de Inquilinos</Typography>
                  <p>Los inquilinos son seleccionados de manera equitativa, teniendo en cuenta los siguientes criterios: los ingresos de cada uno para poder solventar el alquiler, las garantías presentadas, los antecedentes crediticios y los antecedentes penales.</p>

                  <Typography variant="h6">6. Regla de Cálculo</Typography>
                  <p>La puntuación de la aplicación se calcula de manera estándar con la siguiente fórmula:</p>
                  <p>P = (IHB / 1000) * (IAP / 1000) * (CG * 10e-3 + CI * 10e-4) / (ICL)</p>
                  <p>Donde:</p>
                  <ul>
                    <li>P: Puntuación</li>
                    <li>IHB: Índice de Historial Bancario (0 - 1000)</li>
                    <li>IAP: Índice de Antecedentes Policiales (0 - 1000)</li>
                    <li>CG: Componente de Garantías</li>
                    <li>CI: Componente de Ingresos</li>
                    <li>ICL: Índice de contratos de locación</li>
                  </ul>

                  <p>El IHB se encuentra entre 0 y 1000 y se calcula como el promedio de Scoring / 1000 por cada postulante.</p>
                  <p>El IAP se encuentra entre 0 y 1000, y se calcula como el promedio por cada postulante, con descuentos por antecedentes según la siguiente escala:</p>
                  <ul>
                    <li>Antecedente Leve: -400</li>
                    <li>Antecedente Moderado: -600</li>
                    <li>Antecedente Grave: -800</li>
                    <li>Antecedente Crítico: -1000</li>
                  </ul>

                  <p>El Componente de Garantías es el monto de las escrituras de las garantías ingresadas por el Inquilino.</p>
                  <p>El Componente de Ingresos es el Ingreso mensual sumado de los inquilinos.</p>
                  <p>El Índice de contratos de locación se toma del gobierno Argentino de manera mensual.</p>
                  <p>En caso de que dos aplicaciones tengan la misma puntuación, se ponderará aquella que haya sido creada con anterioridad.</p>
                  <p>Se puede ver el histórico en el siguiente link: <a href="https://www.bcra.gob.ar/publicacionesestadisticas/Principales_variables_datos.asp">Historial de Índice de contratos de locación</a>.</p>

                  <Typography variant="h6">7. Análisis Crediticio y de Antecedentes Penales</Typography>
                  <p>Al proponerse como potenciales inquilinos en SGAA, ustedes aceptan recibir un análisis crediticio y de antecedentes penales por parte de proveedores externos autorizados. Toda la información recibida se evalúa internamente y forma parte del sistema de puntuación utilizado para la selección de inquilinos.</p>

                  <Typography variant="h6">8. Políticas de Seguridad</Typography>
                  <p>El SGAA se compromete a mantener la seguridad de los datos personales y financieros de todos los usuarios. Para obtener más información sobre nuestras políticas de seguridad, consulte la sección de Políticas de Seguridad en nuestra plataforma.</p>

                  <Typography variant="h6">9. Contacto</Typography>
                  <p>Para cualquier duda, consulta o sugerencia, pueden ponerse en contacto con nosotros a través del correo electrónico <a href="mailto:sgaa.comunicaciones@gmail.com">sgaa.comunicaciones@gmail.com</a>.</p>

                  <p>Al utilizar el Sistema de Gestión de Alquileres Autónomos (SGAA), ustedes aceptan y se comprometen a cumplir con estos Términos y Condiciones. El incumplimiento de estos términos puede dar lugar a la cancelación de su cuenta en el sistema SGAA.</p>
                </Card>
              </Grid>
            </Grid>
          </Stack>
        </Container>
      </Box>
    </>
  );
}



Page.getLayout = (page) => (
  <DashboardLayout>
    {page}
  </DashboardLayout>
);

export default Page;
