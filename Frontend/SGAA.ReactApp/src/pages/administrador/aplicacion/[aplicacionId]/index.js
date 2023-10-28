import { Box, Container, Stack } from '@mui/material';
import Head from 'next/head';
import { AplicacionDetails } from '/src/sections/aplicacion/aplicacion-detail';
import { Layout as DashboardLayout } from '/src/layouts/dashboard/layout';
import { AuthGuard } from '/src/guards/auth-guard';

const data =
{
  id: 2,
  status: 'AprobacionPendiente',
  inquilinoUsuarioNombreCompleto: 'nombre completo',
  postulaciones: 2,
  puntuacionTotal: 50
};

const Page = () => {
  return (
    <AuthGuard roles={['Administrador']}>
      <Head>
        <title>
          Aplicación
        </title>
      </Head>
      <Box
        component="main"
        sx={{
          flexGrow: 1,
          py: 8
        }}
      >
        <Container maxWidth="xl">
          <Stack spacing={3}>
            <AplicacionDetails aplicacion={data} ></AplicacionDetails>
          </Stack>
        </Container>
      </Box>
    </AuthGuard>
  );
};

Page.getLayout = (page) => (
  <DashboardLayout>
    {page}
  </DashboardLayout>
);

export default Page;
