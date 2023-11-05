import { Box, Card, Container, Grid, Stack, Typography } from '@mui/material';
import Head from 'next/head';
import { FancyBreadcrumbs } from '/src/components/fancy-breadcrumbs';
import { Layout as DashboardLayout } from '/src/layouts/dashboard/layout';

const breadcrumbsConfig = [
  { url: '/inicio', title: 'Inicio' },
  { url: '/privacidad', title: 'Política de Privacidad' }
];

const Page = () => {
  return (
    <>
      <Head>
        <title>
          SGAA - Política de Privacidad
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
                  Política de Privacidad
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
                <Card sx={{ p: 2 }} sx={{
                  border: 1, borderRadius: '8px', 'borderStyle': 'solid', 'borderWidth': '1px', 'borderColor': '#1C2536', p: 2, mt: 1
                }} >

                  <Typography variant="h6">1. Información que recopilamos</Typography>
                  <p>Podemos recopilar información personal identificable, como su nombre, dirección de correo electrónico, dirección postal y otra información que usted nos proporcione voluntariamente cuando utiliza el SGAA. También podemos recopilar información no identificable, como datos demográficos y patrones de uso del sistema.</p>

                  <Typography variant="h6">2. Uso de la información</Typography>
                  <p>Utilizamos su información personal para los siguientes propósitos:</p>
                  <ul>
                    <li>Para proporcionarle los productos y servicios que solicite a través del SGAA.</li>
                    <li>Para enviarle boletines informativos, correos electrónicos promocionales u otra información que pueda interesarle.</li>
                    <li>Para personalizar su experiencia en el SGAA y presentarle contenido y ofertas que se adapten a sus preferencias.</li>
                    <li>Para mejorar el SGAA y nuestros servicios.</li>
                    <li>Para cumplir con las leyes y regulaciones aplicables.</li>
                  </ul>

                  <Typography variant="h6">3. Protección de la información</Typography>
                  <p>Nos comprometemos a proteger su información personal y a implementar medidas de seguridad adecuadas para prevenir el acceso no autorizado, la divulgación, la alteración o la destrucción de su información en el SGAA. Sin embargo, debe ser consciente de que ninguna transmisión de datos a través de Internet es completamente segura, y no podemos garantizar la seguridad de la información que nos proporciona en línea.</p>

                  <Typography variant="h6">4. Compartir información</Typography>
                  <p>No venderemos, alquilaremos ni cederemos su información personal a terceros sin su consentimiento, a menos que estemos obligados por ley a hacerlo. Podemos compartir su información con proveedores de servicios que nos ayudan a operar el SGAA y brindarle servicios.</p>

                  <Typography variant="h6">5. Cookies y tecnologías similares</Typography>
                  <p>Utilizamos cookies y otras tecnologías similares para recopilar información sobre su actividad en el SGAA. Puede administrar sus preferencias de cookies a través de la configuración de su navegador.</p>

                  <Typography variant="h6">6. Enlaces a sitios web de terceros</Typography>
                  <p>El SGAA puede contener enlaces a sitios web de terceros. No somos responsables de las prácticas de privacidad de estos sitios web, por lo que le recomendamos revisar las políticas de privacidad de terceros.</p>

                  <Typography variant="h6">7. Cambios en la política de privacidad</Typography>
                  <p>Nos reservamos el derecho de modificar esta Política de Privacidad en cualquier momento. Le notificaremos sobre cualquier cambio importante a través del SGAA o por otros medios.</p>

                  <Typography variant="h6">8. Póngase en contacto con nosotros</Typography>
                  <p>Si tiene alguna pregunta o inquietud sobre nuestra Política de Privacidad en el SGAA, puede ponerse en contacto con nosotros a través de la dirección de correo electrónico de contacto: <a href="mailto:sgaa.comunicaciones@gmail.com">sgaa.comunicaciones@gmail.com</a>.</p>
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
