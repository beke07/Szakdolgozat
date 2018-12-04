import fontawesome from '@fortawesome/fontawesome'
// Official documentation available at: https://github.com/FortAwesome/vue-fontawesome
import FontAwesomeIcon from '@fortawesome/vue-fontawesome'

// If not present, it won't be visible within the application. Considering that you
// don't want all the icons for no reason. This is a good way to avoid importing too many
// unnecessary things.
fontawesome.library.add(
  require('@fortawesome/fontawesome-free-solid/faEnvelope'),
  require('@fortawesome/fontawesome-free-solid/faGraduationCap'),
  require('@fortawesome/fontawesome-free-solid/faHome'),
  require('@fortawesome/fontawesome-free-solid/faList'),
  require('@fortawesome/fontawesome-free-solid/faSpinner'),
  require('@fortawesome/fontawesome-free-solid/faSignOutAlt'),
  require('@fortawesome/fontawesome-free-solid/faUser'),
  require('@fortawesome/fontawesome-free-solid/faUnlockAlt'),
  require('@fortawesome/fontawesome-free-solid/faCodeBranch'),
  require('@fortawesome/fontawesome-free-solid/faSitemap'),
  require('@fortawesome/fontawesome-free-solid/faInfoCircle'),
  require('@fortawesome/fontawesome-free-solid/faUserEdit'),
  require('@fortawesome/fontawesome-free-solid/faCheck'),
  require('@fortawesome/fontawesome-free-solid/faTrash'),
  require('@fortawesome/fontawesome-free-solid/faPlus'),
  require('@fortawesome/fontawesome-free-solid/faAddressCard'),
  require('@fortawesome/fontawesome-free-solid/faClock'),
  require('@fortawesome/fontawesome-free-solid/faCalendar'),
  require('@fortawesome/fontawesome-free-solid/faArrowUp'),
  require('@fortawesome/fontawesome-free-solid/faArrowDown'),
  require('@fortawesome/fontawesome-free-solid/faChevronLeft'),
  require('@fortawesome/fontawesome-free-solid/faChevronRight'),
  require('@fortawesome/fontawesome-free-solid/faCalendarCheck'),
  require('@fortawesome/fontawesome-free-solid/faTrashAlt'),
  require('@fortawesome/fontawesome-free-solid/faTimesCircle'),
  require('@fortawesome/fontawesome-free-solid/faGavel'),
  require('@fortawesome/fontawesome-free-solid/faTasks'),
  require('@fortawesome/fontawesome-free-solid/faEdit'),
  // Brands
  require('@fortawesome/fontawesome-free-brands/faFontAwesome'),
  require('@fortawesome/fontawesome-free-brands/faMicrosoft'),
  require('@fortawesome/fontawesome-free-brands/faVuejs')
)

export {
  FontAwesomeIcon
}
