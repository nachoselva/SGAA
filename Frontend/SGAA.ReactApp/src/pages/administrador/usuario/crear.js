import { Box, Breadcrumbs, Card, Container, Grid, Link, Stack, Typography } from '@mui/material';
import Head from 'next/head';
import { useRouter } from 'next/router';
import { registrarUsuario } from '/src/api/administrador';
import { AuthGuard } from '/src/guards/auth-guard';
import { Layout as DashboardLayout } from '/src/layouts/dashboard/layout';
import { UsuarioCrearForm } from '/src/sections/usuario/usuario-crear-form';

const Page = () => {
  const router = useRouter();

  const onUsuarioCreated = (result) => {
    if (result)
      router.push('/administrador/usuario');
  };

  return (
    <AuthGuard roles={['Administrador']}>
      <Head>
        <title>
          SGAA - Crear Usuario
        </title>
      </Head>
      <Box>
        <Container maxWidth="xl">
          <Stack spacing={3}>
            <Stack
              direction="row"
              justifyContent="space-between"
              spacing={4}
            >
              <Breadcrumbs aria-label="breadcrumb">
                <Link
                  component="button"
                  underline="hover"
                  color="inherit"
                  onClick={() => router.push('/')}>
                  Inicio
                </Link>
                <Link
                  component="button"
                  underline="hover"
                  color="inherit"
                  onClick={() => router.push('/administrador/usuario')}>
                  Usuarios
                </Link>
                <Link
                  component="button"
                  underline="hover"
                  color="inherit"
                  onClick={() => router.push('/administrador/usuario/crear')}>
                  Crear Usuario
                </Link>
              </Breadcrumbs>
            </Stack>
          </Stack>
        </Container>
      </Box>
      <Box
        component="main"
        sx={{
          flexGrow: 1,
          py: 8
        }}
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
                  Crear Usuario
                </Typography>
              </Stack>
            </Stack>
            <Grid
              container
              spacing={3}
            >
              <Grid
                xs={12}
                sm={8}
                lg={6}
                xl={4}
              >
                <Card sx={{ p: 2 }} >
                  <UsuarioCrearForm handleSubmit={registrarUsuario} handleConfirmationChange={onUsuarioCreated} includeAdminRol={true} />
                </Card>
              </Grid>
            </Grid>
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
