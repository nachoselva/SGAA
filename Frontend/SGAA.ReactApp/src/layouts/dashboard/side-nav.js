import {
  Box, Divider,
  Drawer,
  Stack, Typography,
  useMediaQuery
} from '@mui/material';
import NextLink from 'next/link';
import { usePathname } from 'next/navigation';
import PropTypes from 'prop-types';
import { getMenuItems } from './config';
import { SideNavItem } from './side-nav-item';
import { Logo } from '/src/components/logo';
import { Scrollbar } from '/src/components/scrollbar';

export const SideNav = (props) => {
  const { open, onClose } = props;
  const pathname = usePathname();
  const lgUp = useMediaQuery((theme) => theme.breakpoints.up('lg'));
  const { everyone, administrador, propietario, inquilino, terms } = getMenuItems();

  const renderLink = (item) => {
    let active;
    if (!item.path) {
      active = false;
    }
    else if (item.path === '/') {
      active = item.path === pathname;
    }
    else {
      active = pathname?.startsWith(item.path) ?? false;
    }

    return (
      <SideNavItem
        active={active}
        disabled={item.disabled}
        external={item.external}
        icon={item.icon}
        key={item.title}
        path={item.path}
        title={item.title}
      />
    );
  };

  const content = (
    <Scrollbar
      sx={{
        height: '100%',
        '& .simplebar-content': {
          height: '100%'
        },
        '& .simplebar-scrollbar:before': {
          background: 'neutral.400'
        }
      }}
    >
      <Box
        sx={{
          display: 'flex',
          flexDirection: 'column',
          height: '100%'
        }}
      >
        <Box sx={{ p: 3 }}>
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
          <Box
            sx={{
              alignItems: 'center',
              borderRadius: 1,
              display: 'flex',
              justifyContent: 'space-between',
              mt: 2,
              p: '12px'
            }}
          >
            <div>
              <Typography
                color="inherit"
                variant="subtitle1">
                Sistema de gestión de Alquileres Autónomos
              </Typography>
            </div>
          </Box>
        </Box>
        <Divider sx={{ borderColor: 'neutral.700' }} />
        <Box
          component="nav"
          sx={{
            flexGrow: 1,
            px: 2,
            py: 3
          }}
        >
          <Stack
            component="ul"
            spacing={0.5}
            sx={{
              listStyle: 'none',
              p: 0,
              m: 0
            }}
          >
            {
              everyone.map((item) => renderLink(item))
            }
            <Divider sx={{ borderColor: 'neutral.700' }} />
            {
              administrador.length > 0 &&
              <Box><p>Administrador</p></Box>
            }
            {
              administrador.map((item) => renderLink(item))
            }
            {
              administrador.length > 0 &&
              < Divider sx={{ borderColor: 'neutral.700' }} />
            }
            {
              propietario.length > 0 &&
              <Box><p>Propietario</p></Box>
            }
            {
              propietario.map((item) => renderLink(item))
            }
            {
              propietario.length > 0 &&
              <Divider sx={{ borderColor: 'neutral.700' }} />
            }
            {
              inquilino.length > 0 &&
              <Box><p>Inquilino</p></Box>
            }
            {
              inquilino.map((item) => renderLink(item))
            }
            {
              inquilino.length > 0 &&
              <Divider sx={{ borderColor: 'neutral.700' }} />
            }
            {
              terms.map((item) => renderLink(item))
            }
            {
              terms.length > 0 &&
              <Divider sx={{ borderColor: 'neutral.700' }} />
            }
          </Stack>
        </Box>
        <Divider sx={{ borderColor: 'neutral.700' }} />
      </Box>
    </Scrollbar>
  );

  if (lgUp) {
    return (
      <Drawer
        anchor="left"
        open
        PaperProps={{
          sx: {
            backgroundColor: 'neutral.800',
            color: 'common.white',
            width: 280
          }
        }}
        variant="permanent"
      >
        {content}
      </Drawer>
    );
  }

  return (
    <Drawer
      anchor="left"
      onClose={onClose}
      open={open}
      PaperProps={{
        sx: {
          backgroundColor: 'neutral.800',
          color: 'common.white',
          width: 280
        }
      }}
      sx={{ zIndex: (theme) => theme.zIndex.appBar + 100 }}
      variant="temporary"
    >
      {content}
    </Drawer>
  );
};

SideNav.propTypes = {
  onClose: PropTypes.func,
  open: PropTypes.bool
};
