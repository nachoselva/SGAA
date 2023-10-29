import { Box, Unstable_Grid2 as Grid } from '@mui/material';
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
        >
          <Box
            component="header"
            sx={{
              left: 0,
              p: 3,
              position: 'fixed',
              top: 0,
              width: '100%'
            }}
          >
            <Box
              component={NextLink}
              href="/"
              sx={{
                display: 'inline-flex',
                height: 32,
                width: 32
              }}
            >
              <Logo />
            </Box>
          </Box>
          {children}
      </Grid>
    </Box>
  );
};

Layout.prototypes = {
  children: PropTypes.node
};