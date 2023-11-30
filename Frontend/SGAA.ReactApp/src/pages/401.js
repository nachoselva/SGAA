import ArrowLeftIcon from '@heroicons/react/24/solid/ArrowLeftIcon';
import { Box, Button, Container, SvgIcon, Typography } from '@mui/material';
import Head from 'next/head';
import NextLink from 'next/link';

const Page = () => (
  <>
    <Head>
      <title>
        401
      </title>
    </Head>
    <Box
      component="main"
      sx={{ alignItems: 'center', display: 'flex', flexGrow: 1, minHeight: '100%' }}
    >
      <Container maxWidth="md">
        <Box
          sx={{ alignItems: 'center', display: 'flex', flexDirection: 'column' }}
        >
          <Box
            sx={{ mb: 3, textAlign: 'center' }}
          >
            <img
              alt="Under development"
              src="/assets/errors/error-404.png"
              style={{
                display: 'inline-block',
                maxWidth: '100%',
                width: 400
              }}
            />
          </Box>
          <Typography
            align="center"
            sx={{ mb: 3 }}
            variant="h3"
          >
            401: Acceso no autenticado
          </Typography>
          <Typography
            align="center"
            color="text.secondary"
            variant="body1"
          >
            Debe loguearse para poder ingresar al sistema.
          </Typography>
          <Button
            component={NextLink}
            href="/auth/login"
            startIcon={(
              <SvgIcon fontSize="small">
                <ArrowLeftIcon />
              </SvgIcon>
            )}
            sx={{ mt: 3 }}
            variant="contained"
          >
            Ir a Login
          </Button>
        </Box>
      </Container>
    </Box>
  </>
);

export default Page;
