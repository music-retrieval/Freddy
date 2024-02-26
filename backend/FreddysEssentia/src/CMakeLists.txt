# Define the C++ executable
add_library(FreddysEssentia SHARED main.cpp)

# Link against Essentia libraries
target_link_libraries(FreddysEssentia essentia)
