import { Box, Card, Container, Grid, Stack, Typography } from '@mui/material';
import Head from 'next/head';
import { FancyBreadcrumbs } from './fancy-breadcrumbs';
import { AuthGuard } from '/src/guards/auth-guard';

export const FancyFormPage = (props) => {
  const { title, form, breadcrumbsConfig, roles } = props;

  return (
    <AuthGuard roles={roles}>
      <Head>
        <title>
          SGAA - {title}
        </title>
      </Head>
      <FancyBreadcrumbs breadcrumbsConfig={breadcrumbsConfig}>
      </FancyBreadcrumbs>
      <Box
        component="main"
        sx={{
          flexGrow: 1
        }}
      >
        <Container maxWidth="xl" >
          <Stack spacing={3} >
            <Stack
              direction="row"
              justifyContent="space-between"
              spacing={4}
            >
              <Stack spacing={1}>
                <Typography variant="h4">
                  {title}
                </Typography>
              </Stack>
            </Stack>
            <Grid
              container
              spacing={3}
            >
              <Grid
                xs={12}
                lg={8}
              >
                <Card sx={{ p: 2 }} sx={{
                  border: 1, borderRadius: '8px', 'border- style': 'solid', 'border-width': '1px', 'border-color': '#1C2536', p: 2, mt: 1
                }} >
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


