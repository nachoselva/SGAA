import { Box, Container, Stack, Typography } from '@mui/material';
import Head from 'next/head';
import { AuthGuard } from '/src/guards/auth-guard';
import { Layout as DashboardLayout } from '/src/layouts/dashboard/layout';

const Page = () => (
  <AuthGuard>
    <Head>
      <title>
        Inicio
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
          <Stack
            direction="row"
            justifyContent="space-between"
            spacing={4}
          >
            <Stack spacing={1}>
              <Typography variant="h4">
                Inicio
              </Typography>
            </Stack>
          </Stack>
        </Stack>
      </Container>
    </Box>
  </AuthGuard>
);

Page.getLayout = (page) => (
  <DashboardLayout>
    {page}
  </DashboardLayout>
);

export default Page;
