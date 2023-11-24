import { Box, Card, Container, Grid, Link, Stack, Typography } from '@mui/material';
import Head from 'next/head';
import { FancyBreadcrumbs } from '/src/components/fancy-breadcrumbs';
import { useAuthContext } from '/src/contexts/auth-context';
import { Layout as DashboardLayout } from '/src/layouts/dashboard/layout';

const breadcrumbsConfig = [
  { url: '/inicio', title: 'Inicio' },
  { url: '/ayuda', title: 'Ayuda' }
];

const Page = () => {
  const { isAuthenticated, user } = useAuthContext();

  return (
    <>
      <Head>
        <title>
          SGAA - Ayuda
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
                  Ayuda
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
                  {
                    isAuthenticated &&
                    user.roles.includes('Administrador') &&
                      <Typography variant="h6">
                        <Link href="https://docs.google.com/document/d/18yEgJ35MHSVRbCePhM07ZzKMmcEJhH5v11Q_y7yXQLI/edit?usp=sharing">
                          Manual para Administradores
                        </Link>
                      </Typography>
                  }

                  {
                    isAuthenticated &&
                    user.roles.includes('Propietario') &&
                    <>
                      <Typography variant="h6">
                        <Link href="https://docs.google.com/document/d/1gN2FlXihrxRXD69zGOaxsqEoSq4Neibe8KjpBmYl3BI/edit?usp=sharing">
                          Manual para Propietarios
                        </Link>
                      </Typography>
                    </>
                  }

                  {
                    isAuthenticated &&
                    user.roles.includes('Inquilino') &&
                    <>
                      <Typography variant="h6">
                        <Link href="https://docs.google.com/document/d/1YND7EnxFOdrpIAR1-K9CS6EhDOzBHd00mkEXJ3Gr3mw/edit?usp=sharing">
                          Manual para Inquilinos
                        </Link>
                      </Typography>
                    </>
                  }

                  <Typography variant="h6">
                    <Link href="https://docs.google.com/document/d/1jcLbfXHT1XpjmE8r_mdFx0zpGlv1jbjPtWyUWjWHag0/edit?usp=sharing">
                      Manual para Usuarios Públicos
                    </Link>
                  </Typography>
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
