cmake_minimum_required (VERSION 3.10)

project ("Hsp.AbletonLink.Native")

# Add source to this project's executable.
add_library (link SHARED "Hsp.AbletonLink.Native.cpp" "MyAbletonLink.cpp")

target_include_directories(link PRIVATE "link/include")
target_include_directories(link PRIVATE "link/modules/asio-standalone/asio/include")

if (CMAKE_VERSION VERSION_GREATER 3.12)
  set_property(TARGET link PROPERTY CXX_STANDARD 20)
endif()