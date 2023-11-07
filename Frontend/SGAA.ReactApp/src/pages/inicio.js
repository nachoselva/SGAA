import { Box, Card, Container, Grid, Stack, Typography } from '@mui/material';
import Head from 'next/head';
import { FancyBreadcrumbs } from '/src/components/fancy-breadcrumbs';
import { Layout as DashboardLayout } from '/src/layouts/dashboard/layout';

const breadcrumbsConfig = [
  { url: '/inicio', title: 'Inicio' }
];

const Page = () => {
  return (
    <>
      <Head>
        <title>
          SGAA - Términos y Condiciones
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
                  Sistema de Gestión de Alquileres Autónomos (SGAA)
                </Typography>
              </Stack>
            </Stack>
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
