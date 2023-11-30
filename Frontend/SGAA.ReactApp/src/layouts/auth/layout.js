import { Box, Grid, Typography } from '@mui/material';
import NextLink from 'next/link';
import PropTypes from 'prop-types';
import { Logo } from '/src/components/logo';

export const Layout = (props) => {
  const { children } = props;

  return (
    <Box
      component="main"
    >
      <Grid
        id="test-2"
        sx={{ height: '100%' }}
      >
        <Box
          component="header"
          sx={{ left: 0, p: 3, position: 'fixed', top: 0, width: '100%' }}
        >
          <Box
            component={NextLink}
            href="/"
            sx={{ display: 'inline-flex', height: 32, width: 32 }}
          >
            <Logo />
          </Box>
        </Box>

        <Box
          sx={{ backgroundColor: 'background.paper', flex: '1 1 auto', alignItems: 'center', display: 'flex', flexDirection: 'column', height: '100%' }}
        >
          <Typography variant="h1" sx={{ mt: 10, maxWidth: 700 }}>
            Sistema de gestión de alquileres autónomos
          </Typography>
          <Box
            sx={{ maxWidth: 550, px: 3, py: '100px', width: '100%' }}
          >
            {children}
          </Box>
        </Box>
      </Grid>
    </Box>
  );
};

Layout.prototypes = {
  children: PropTypes.node
};