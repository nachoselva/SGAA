import { Box, Card, Container, Grid, Stack, Typography } from '@mui/material';
import Head from 'next/head';
import { FancyBreadcrumbs } from './fancy-breadcrumbs';
import { AuthGuard } from '/src/guards/auth-guard';

export const FancyFormPage = (props) => {
  const { entityName, form, breadcrumbsConfig } = props;
  console.log(entityName);
  console.log(breadcrumbsConfig);
  return (
    <AuthGuard roles={['Administrador']}>
      <Head>
        <title>
          SGAA - Detalle {entityName}
        </title>
      </Head>
      <FancyBreadcrumbs breadcrumbsConfig={breadcrumbsConfig}>
      </FancyBreadcrumbs>
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
                  Detalle Usuario
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
                  {
                    form
                  }
                </Card>
              </Grid>
            </Grid>
          </Stack>
        </Container>
      </Box>
    </AuthGuard>
  );
};


