import Bars3Icon from '@heroicons/react/24/solid/Bars3Icon';
import UserCircleIcon from '@heroicons/react/24/solid/UserCircleIcon';
import {
    Avatar, Box, IconButton, Stack, SvgIcon, useMediaQuery
} from '@mui/material';
import Divider from '@mui/material/Divider';
import { alpha } from '@mui/material/styles';
import PropTypes from 'prop-types';
import { AccountPopover } from './account-popover';
import { usePopover } from '/src/hooks/use-popover';


const SIDE_NAV_WIDTH = 280;
const TOP_NAV_HEIGHT = 64;

export const TopNav = (props) => {
  const { onNavOpen } = props;
  const accountPopover = usePopover();
  const lgUp = useMediaQuery((theme) => theme.breakpoints.up('lg'));

  return (
    <>
      <Box
        component="header"
        sx={{
          backdropFilter: 'blur(6px)',
          backgroundColor: (theme) => alpha(theme.palette.background.default, 0.8),
          position: 'sticky',
          left: {
            lg: `${SIDE_NAV_WIDTH}px`
          },
          top: 0,
          width: {
            lg: `calc(100% - ${SIDE_NAV_WIDTH}px)`
          },
          zIndex: (theme) => theme.zIndex.appBar
        }}
      >
        <Stack
          alignItems="center"
          direction="row"
          justifyContent="space-between"
          spacing={2}
          sx={{
            minHeight: TOP_NAV_HEIGHT,
            px: 2
          }}
        >
          <Stack
          >
            {!lgUp && (
              <IconButton onClick={onNavOpen}>
                <SvgIcon fontSize="small">
                  <Bars3Icon />
                </SvgIcon>
              </IconButton>
            )}
          </Stack>
          <Stack
            alignItems="center"
            direction="row"
            spacing={2}
          >
            <Avatar
              onClick={accountPopover.handleOpen}
              ref={accountPopover.anchorRef}
              sx={{
                cursor: 'pointer',
                height: 40,
                width: 40
              }}>
              <UserCircleIcon />
            </Avatar>
          </Stack>
        </Stack>
        <Divider sx={{
          borderBottomWidth: 5
        }} />
      </Box>
      <AccountPopover
        anchorEl={accountPopover.anchorRef.current}
        open={accountPopover.open}
        onClose={accountPopover.handleClose}
      />
    </>
  );
};

TopNav.propTypes = {
  onNavOpen: PropTypes.func
};
