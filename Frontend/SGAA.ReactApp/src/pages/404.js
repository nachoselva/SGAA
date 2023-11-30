import ArrowLeftIcon from '@heroicons/react/24/solid/ArrowLeftIcon';
import { Box, Button, Container, SvgIcon, Typography } from '@mui/material';
import Head from 'next/head';
import NextLink from 'next/link';

const Page = () => (
  <>
    <Head>
      <title>
        404 | Devias Kit
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
            404: Página inexistente
          </Typography>
          <Typography
            align="center"
            color="text.secondary"
            variant="body1"
          >
            Esta página no existe. En caso que sea un error, por favor contacte al administrador del sitio.
          </Typography>
          <Button
            component={NextLink}
            href="/"
            startIcon={(
              <SvgIcon fontSize="small">
                <ArrowLeftIcon />
              </SvgIcon>
            )}
            sx={{ mt: 3 }}
            variant="contained"
          >
            Ir a Inicio
          </Button>
        </Box>
      </Container>
    </Box>
  </>
);

export default Page;
